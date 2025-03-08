using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public int mainSceneIndex = 0;
    public int gameSceneIndex = 0;



    private void Awake()
    {
        // Singleton pattern to ensure only one AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }



    
    void Update()
    {
        
    }

    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    
    public void LoadScene(int scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }


    public void RestartGame()
    {

        ReloadScene();
    }

    private void ReloadScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);   
    }

    public void LoadMainScene()
    {
        LoadScene(mainSceneIndex);
    }
    
    public void LoadGameScene()
    {
        LoadScene(mainSceneIndex);
    }
}
