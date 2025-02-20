using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private float waitTime = 5f; // 대기 시간
    private float countdownDuration = 10f; // 카운트다운 시간
    private bool isCountingDown = true; // 카운트다운 상태
    private bool isWaiting = false; // 대기 상태

    void Start()
    {
        elapsedTime = 0f; // 초기화
    }

    void Update()
    {
        if (isCountingDown)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= countdownDuration)
            {
                // 카운트다운이 끝났을 때
                isCountingDown = false;
                isWaiting = true;
                elapsedTime = 0f; // 대기 시간 동안은 시간 초기화
            }
        }
        else if (isWaiting)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= waitTime)
            {
                // 대기 시간이 끝났을 때
                isWaiting = false;
                isCountingDown = true; // 다시 카운트다운 시작
                elapsedTime = 0f; // 카운트다운 시간 초기화
            }
        }

        // 타이머 텍스트 업데이트
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}