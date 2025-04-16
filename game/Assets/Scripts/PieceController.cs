using UnityEngine;
using System.Collections.Generic;

public class PieceController : MonoBehaviour
{
    [Header("Cài đặt di chuyển")]
    public float tileSize = 1f;           // Kích thước mỗi bước di chuyển
    public LayerMask obstacleLayer;       // Lớp chướng ngại vật
    public LayerMask pieceLayer;          // Lớp các mảnh ghép
    public float moveSpeed = 10f;         // Tốc độ di chuyển

    private Vector2 targetPosition;       // Vị trí mục tiêu tiếp theo
    private bool isMoving = false;        // Đánh dấu piece đang di chuyển

    // Danh sách quản lý tất cả các piece
    private static List<PieceController> allPieces = new List<PieceController>();
    private static bool processingInput = false;

    private Vector2 startTouchPosition; // Tọa độ bắt đầu khi chạm màn hình
    private Vector2 endTouchPosition;   // Tọa độ kết thúc khi chạm màn hình
    private float swipeThreshold = 50f; // Ngưỡng để xác định vuốt (độ dài tối thiểu)

    void Awake()
    {
        // Thêm piece này vào danh sách quản lý
        if (!allPieces.Contains(this))
        {
            allPieces.Add(this);
        }
    }

    void OnDestroy()
    {
        // Loại bỏ piece khỏi danh sách khi bị hủy
        if (allPieces.Contains(this))
        {
            allPieces.Remove(this);
        }
    }

    void Start()
    {
        targetPosition = transform.position; // Vị trí mục tiêu ban đầu
    }

    void Update()
    {
        // Kiểm tra và xử lý di chuyển nếu không có piece nào đang di chuyển
        if (!processingInput)
        {
            bool anyMoving = false;
            foreach (PieceController piece in allPieces)
            {
                if (Vector2.Distance(piece.transform.position, piece.targetPosition) > 0.01f)
                {
                    anyMoving = true;
                    break;
                }
            }

            if (!anyMoving)
            {
                Vector2 direction = GetInputDirection();
                if (direction != Vector2.zero)
                {
                    processingInput = true;
                    MovePiecesInDirection(direction);
                    processingInput = false;
                }
            }
        }

        // Di chuyển piece đến vị trí mục tiêu
        if (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private static void MovePiecesInDirection(Vector2 direction)
    {
        // Sắp xếp các piece theo hướng di chuyển
        List<PieceController> sortedPieces = new List<PieceController>(allPieces);
        if (direction == Vector2.up)
        {
            sortedPieces.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y)); // Từ trên xuống
        }
        else if (direction == Vector2.down)
        {
            sortedPieces.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y)); // Từ dưới lên
        }
        else if (direction == Vector2.right)
        {
            sortedPieces.Sort((a, b) => b.transform.position.x.CompareTo(a.transform.position.x)); // Từ phải sang trái
        }
        else if (direction == Vector2.left)
        {
            sortedPieces.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x)); // Từ trái sang phải
        }

        // Duyệt từng piece và xác định khả năng di chuyển
        bool allowGroupMovement = true; // Nếu piece ngoài cùng có thể di chuyển, bỏ qua kiểm tra pieceLayer
        foreach (PieceController piece in sortedPieces)
        {
            Vector2 newPosition = (Vector2)piece.transform.position + direction * piece.tileSize;

            if (allowGroupMovement)
            {
                // Chỉ kiểm tra obstacleLayer, không kiểm tra pieceLayer
                if (!piece.IsBlockedByObstacle(newPosition))
                {
                    piece.targetPosition = newPosition;
                }
                else
                {
                    allowGroupMovement = false; // Nếu piece ngoài cùng không thể di chuyển, kích hoạt kiểm tra pieceLayer cho các piece còn lại
                }
            }

            if (!allowGroupMovement)
            {
                // Kiểm tra cả obstacleLayer và pieceLayer
                if (!piece.IsBlocked(newPosition))
                {
                    piece.targetPosition = newPosition;
                }
            }
        }
    }

    private Vector2 GetInputDirection()
    {
        // **Kiểm tra input từ phím máy tính**
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;

        // **Kiểm tra vuốt trên điện thoại (mobile swipe detection)**
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Lấy thông tin của lần chạm đầu tiên

            if (touch.phase == TouchPhase.Began)
            {
                // Lưu vị trí bắt đầu của lần chạm
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Lưu vị trí kết thúc của lần chạm
                endTouchPosition = touch.position;

                // Tính toán độ dài vuốt
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                // Kiểm tra ngưỡng để xác định hướng vuốt
                if (swipeDelta.magnitude >= swipeThreshold)
                {
                    // Vuốt theo trục X
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x > 0) return Vector2.right; // Vuốt sang phải
                        else return Vector2.left;                  // Vuốt sang trái
                    }
                    // Vuốt theo trục Y
                    else
                    {
                        if (swipeDelta.y > 0) return Vector2.up;   // Vuốt lên trên
                        else return Vector2.down;                 // Vuốt xuống dưới
                    }
                }
            }
        }

        // Không có input nào
        return Vector2.zero;
    }

    private bool IsBlocked(Vector2 position)
    {
        // Kiểm tra obstacleLayer
        Collider2D obstacle = Physics2D.OverlapPoint(position, obstacleLayer);
        if (obstacle != null)
        {
            return true;
        }

        // Kiểm tra pieceLayer
        Collider2D piece = Physics2D.OverlapPoint(position, pieceLayer);
        if (piece != null && piece.gameObject != gameObject)
        {
            return true;
        }

        return false;
    }

    private bool IsBlockedByObstacle(Vector2 position)
    {
        // Chỉ kiểm tra obstacleLayer
        Collider2D obstacle = Physics2D.OverlapPoint(position, obstacleLayer);
        return obstacle != null;
    }
}