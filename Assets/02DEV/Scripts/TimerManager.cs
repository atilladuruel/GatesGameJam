using UnityEngine;
using TMPro;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float realTimeDuration = 600f; // 10 dakika (600 saniye)
    private float gameTimeStart = 8f * 60f; // 08:00 ba�lang�� (dakika cinsinden)
    private float gameTimeEnd = 18f * 60f; // 18:00 biti� (dakika cinsinden)
    private float currentGameTime;
    private bool isWarningActive = false;

    void Start()
    {
        currentGameTime = gameTimeStart;
        StartCoroutine(UpdateGameTime());
    }

    IEnumerator UpdateGameTime()
    {
        float startTime = Time.time;

        while (currentGameTime < gameTimeEnd)
        {
            float elapsedRealTime = Time.time - startTime;
            float progress = elapsedRealTime / realTimeDuration;
            currentGameTime = Mathf.Lerp(gameTimeStart, gameTimeEnd, progress);

            UpdateTimerUI();

            // E�er saat 17:30'u ge�tiyse ve uyar� hen�z aktif de�ilse, uyar�y� ba�lat
            if (currentGameTime >= 17.5f * 60f && !isWarningActive)
            {
                isWarningActive = true;
                StartCoroutine(StartWarningEffect());
            }

            yield return null;
        }
    }

    void UpdateTimerUI()
    {
        int hours = Mathf.FloorToInt(currentGameTime / 60f);
        int minutes = Mathf.FloorToInt(currentGameTime % 60f);
        timerText.text = $"Time: {hours:00}:{minutes:00}";
    }

    IEnumerator StartWarningEffect()
    {
        while (currentGameTime < gameTimeEnd)
        {
            // K�rm�z� renk ve b�y�me/k���lme efekti
            timerText.color = Color.red;
            timerText.transform.localScale = Vector3.one * 1.2f;
            yield return new WaitForSeconds(0.5f);

            timerText.color = Color.white;
            timerText.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
