using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private float waitTime = 5f; // ��� �ð�
    private float countdownDuration = 10f; // ī��Ʈ�ٿ� �ð�
    private bool isCountingDown = true; // ī��Ʈ�ٿ� ����
    private bool isWaiting = false; // ��� ����

    void Start()
    {
        elapsedTime = 0f; // �ʱ�ȭ
    }

    void Update()
    {
        if (isCountingDown)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= countdownDuration)
            {
                // ī��Ʈ�ٿ��� ������ ��
                isCountingDown = false;
                isWaiting = true;
                elapsedTime = 0f; // ��� �ð� ������ �ð� �ʱ�ȭ
            }
        }
        else if (isWaiting)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= waitTime)
            {
                // ��� �ð��� ������ ��
                isWaiting = false;
                isCountingDown = true; // �ٽ� ī��Ʈ�ٿ� ����
                elapsedTime = 0f; // ī��Ʈ�ٿ� �ð� �ʱ�ȭ
            }
        }

        // Ÿ�̸� �ؽ�Ʈ ������Ʈ
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int min = Mathf.FloorToInt(elapsedTime / 60);
        int sec = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}