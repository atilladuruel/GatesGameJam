using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton kontrol�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�se bile yok olmas�n
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

    public void ActivateDeactivateUI(GameObject UIObject, Animator animator,string parameterName, float waitSec, bool on)
    {
        animator.SetTrigger(parameterName);
        StartCoroutine(WaitCoroutine(waitSec));
        UIObject.SetActive(on);
    }

    public IEnumerator WaitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

}
