using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance;
    public float timeRemaining = 45f;
    public TextMeshProUGUI timerText;
    private bool isRunning = true;

    private void Awake()
    {
            Instance = this;
    }
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    isRunning = false;
                    GameOver();
                }
            }
            else
            {
                timeRemaining = 0;
                isRunning = false;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeRemaining);
        timerText.text = "00 : "+ seconds.ToString();
    }

    void GameOver()
    {
        Debug.Log("Hết thời gian! Trò chơi kết thúc.");
        GameManager.Instance.state = GameManager.GameState.Loss;
    }
    public void ResetTimer()
    {
        timeRemaining = 45f; // Đặt lại thời gian
        isRunning = true;    // Cho phép đếm ngược hoạt động
        UpdateTimerDisplay(); // Cập nhật hiển thị lại thời gian
    }
}
