using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne deðiþse bile yok olmasýn
        }
        else
        {
            Destroy(gameObject); // Zaten bir UIManager varsa, yenisini yok et
        }
    }

    public void StartGame()
    {
        SceneManager.Instance.LoadGameScene();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ActivateDeactivateUI(GameObject UIObject, bool on)
    {
        UIObject.SetActive(on);
    }

    public void AfterActivateDeactivateUI(GameObject UIObject, Animator animator, string parameterName, float waitSec, bool on)
    {
        animator.SetTrigger(parameterName);
        StartCoroutine(WaitCoroutine(waitSec));
        UIObject.SetActive(on);
    }
    public void BeforeActivateDeactivateUI(GameObject UIObject, Animator animator, string parameterName, float waitSec, bool on)
    {
        UIObject.SetActive(on);
        StartCoroutine(WaitCoroutine(waitSec));
        animator.SetTrigger(parameterName);
    }

    public IEnumerator WaitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public void PlayBounce(Image targetImage, float duration = 0.5f, float scaleFactor = 1.2f)
    {
        if (targetImage == null)
        {
            Debug.LogWarning("Target Image is not assigned!");
            return;
        }

        // Ýlk olarak büyüt, sonra orijinal boyutuna getir
        targetImage.transform.DOScale(scaleFactor, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                targetImage.transform.DOScale(1f, duration / 2).SetEase(Ease.InQuad);
            });
    }
    public void PlayBounce(Image targetImage)
    {
        PlayBounce(targetImage, 0.2f, 1.1f);
    }

}
