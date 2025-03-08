using UnityEngine;
using TMPro;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float realTimeDuration = 600f; // 10 dakika (600 saniye)
    private float gameTimeStart = 8f * 60f; // 08:00 baþlangýç (dakika cinsinden)
    private float gameTimeEnd = 18f * 60f; // 18:00 bitiþ (dakika cinsinden)
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

            // Eðer saat 17:30'u geçtiyse ve uyarý henüz aktif deðilse, uyarýyý baþlat
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
            // Kýrmýzý renk ve büyüme/küçülme efekti
            timerText.color = Color.red;
            timerText.transform.localScale = Vector3.one * 1.2f;
            yield return new WaitForSeconds(0.5f);

            timerText.color = Color.white;
            timerText.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
