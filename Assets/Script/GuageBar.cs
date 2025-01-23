using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    public Slider gaugeBar; // Slider를 연결
    public float maxValue = 100f; // 최대 값
    private float currentValue; // 현재 값

    void Start()
    {
        currentValue = maxValue; // 초기 값 설정
        UpdateGauge();
    }

    void Update()
    {
        // 예시: 1초에 10씩 감소
        if (currentValue > 0)
        {
            currentValue -= 10 * Time.deltaTime;
            UpdateGauge();
        }
    }

    public void UpdateGauge()
    {
        gaugeBar.value = currentValue / maxValue; // 값 업데이트
    }

    public void AddGauge(float value)
    {
        currentValue = Mathf.Clamp(currentValue + value, 0, maxValue);
        UpdateGauge();
    }

    public void SubtractGauge(float value)
    {
        currentValue = Mathf.Clamp(currentValue - value, 0, maxValue);
        UpdateGauge();
    }
}