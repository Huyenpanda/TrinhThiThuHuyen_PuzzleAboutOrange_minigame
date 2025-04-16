using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceChecker : MonoBehaviour
{
    public static PieceChecker instance;

    [Header("Puzzle Pieces")]
    public GameObject topLeftPiece;
    public GameObject topRightPiece;
    public GameObject bottomLeftPiece;
    public GameObject bottomRightPiece;

    [Header("Settings")]
    public float tolerance = 0.5f;  // Dung sai cho phép khi kiểm tra vị trí tương đối
    public bool showDebugInfo = true;

    // Biến để tránh trigger liên tục khi win
    private bool hasTriggeredWin = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        // Chỉ kiểm tra nếu chưa trigger win và đang trong trạng thái Game
        if (!hasTriggeredWin)
        {
            if (CheckPuzzleArrangement())
            {
                Debug.Log("Puzzle đã được sắp xếp đúng! Chiến thắng!");
                hasTriggeredWin = true;
                StartCoroutine(ProceedToNextLevel());
            }
        }
    }


    private IEnumerator ProceedToNextLevel()
    {
        yield return new WaitForSeconds(0.5f); // Hiệu ứng chờ (tuỳ chỉnh)
        GameManager.Instance.state = GameManager.GameState.Pass;
        Debug.Log("All players are at the gates! Proceeding to next level.");
    }

    private bool CheckPuzzleArrangement()
    {
        // Kiểm tra các mảnh có tồn tại không
        if (topLeftPiece == null || topRightPiece == null ||
            bottomLeftPiece == null || bottomRightPiece == null)
            return false;

        // Lấy vị trí của các mảnh
        Vector2 topLeft = topLeftPiece.transform.position;
        Vector2 topRight = topRightPiece.transform.position;
        Vector2 bottomLeft = bottomLeftPiece.transform.position;
        Vector2 bottomRight = bottomRightPiece.transform.position;

        // Kiểm tra topLeft có ở phía trên và bên trái so với bottomRight
        bool topLeftCorrect = topLeft.y > bottomRight.y && topLeft.x < bottomRight.x;

        // Kiểm tra topRight có ở phía trên và bên phải so với bottomLeft
        bool topRightCorrect = topRight.y > bottomLeft.y && topRight.x > bottomLeft.x;

        // Kiểm tra các mối quan hệ theo chiều ngang
        bool topRowCorrect = IsHorizontallyAligned(topLeft, topRight) && topLeft.x < topRight.x;
        bool bottomRowCorrect = IsHorizontallyAligned(bottomLeft, bottomRight) && bottomLeft.x < bottomRight.x;

        // Kiểm tra các mối quan hệ theo chiều dọc
        bool leftColumnCorrect = IsVerticallyAligned(topLeft, bottomLeft) && topLeft.y > bottomLeft.y;
        bool rightColumnCorrect = IsVerticallyAligned(topRight, bottomRight) && topRight.y > bottomRight.y;

        // Debug
        if (showDebugInfo)
        {
            Debug.Log($"TopLeft correct: {topLeftCorrect}");
            Debug.Log($"TopRight correct: {topRightCorrect}");
            Debug.Log($"Top row correct: {topRowCorrect}");
            Debug.Log($"Bottom row correct: {bottomRowCorrect}");
            Debug.Log($"Left column correct: {leftColumnCorrect}");
            Debug.Log($"Right column correct: {rightColumnCorrect}");
        }

        // Trả về true nếu tất cả điều kiện đều đúng
        return topLeftCorrect && topRightCorrect &&
               topRowCorrect && bottomRowCorrect &&
               leftColumnCorrect && rightColumnCorrect;
    }

    // Kiểm tra hai điểm có gần như cùng vị trí Y (cùng hàng) không
    private bool IsHorizontallyAligned(Vector2 point1, Vector2 point2)
    {
        return Mathf.Abs(point1.y - point2.y) <= tolerance;
    }

    // Kiểm tra hai điểm có gần như cùng vị trí X (cùng cột) không
    private bool IsVerticallyAligned(Vector2 point1, Vector2 point2)
    {
        return Mathf.Abs(point1.x - point2.x) <= tolerance;
    }

    // Vẽ gizmos để debug trong Editor
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (topLeftPiece && topRightPiece)
        {
            Gizmos.color = IsHorizontallyAligned(topLeftPiece.transform.position, topRightPiece.transform.position) ? Color.green : Color.red;
            Gizmos.DrawLine(topLeftPiece.transform.position, topRightPiece.transform.position);
        }

        if (bottomLeftPiece && bottomRightPiece)
        {
            Gizmos.color = IsHorizontallyAligned(bottomLeftPiece.transform.position, bottomRightPiece.transform.position) ? Color.green : Color.red;
            Gizmos.DrawLine(bottomLeftPiece.transform.position, bottomRightPiece.transform.position);
        }

        if (topLeftPiece && bottomLeftPiece)
        {
            Gizmos.color = IsVerticallyAligned(topLeftPiece.transform.position, bottomLeftPiece.transform.position) ? Color.green : Color.red;
            Gizmos.DrawLine(topLeftPiece.transform.position, bottomLeftPiece.transform.position);
        }

        if (topRightPiece && bottomRightPiece)
        {
            Gizmos.color = IsVerticallyAligned(topRightPiece.transform.position, bottomRightPiece.transform.position) ? Color.green : Color.red;
            Gizmos.DrawLine(topRightPiece.transform.position, bottomRightPiece.transform.position);
        }
    }
}