using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameMusicManager musicManager;
    private bool isTransitioning = false;

    [SerializeField] private float delayTime = 1f;

    [HideInInspector] public int savedLevel = 0;
    [HideInInspector] public int totalLevel;
    [HideInInspector] public bool isGameBeaten = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void GameOver()
    {
        StartCoroutine(LoadSceneWithDelay("Result"));
    }

    public void Restart()
    {
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void Home()
    {
        if (isTransitioning) return;
        isTransitioning = true;
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void GoToNextLevel()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        UnlockNextLevel(savedLevel);
        savedLevel++;

        if (savedLevel >= totalLevel)
        {
            isGameBeaten = true;
            savedLevel = 0;
            StartCoroutine(LoadSceneWithDelay("MainMenu"));
            return;
        }
        

        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayTime);

        if (musicManager != null)
        {
            musicManager.FadeOutAndStop();
        }

        SceneTransition transition = FindFirstObjectByType<SceneTransition>();

        if (transition != null)
        {
            transition.StartTransition(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void UnlockNextLevel(int completedLevelIndex)
    {
        int nextLevel = completedLevelIndex + 1;

        if (nextLevel >= totalLevel)
            return;

        if (nextLevel > PlayerPrefs.GetInt("MaxLevelUnlocked", 0))
        {
            PlayerPrefs.SetInt("MaxLevelUnlocked", nextLevel);
            PlayerPrefs.Save();
        }
    }

    public int GetMaxLevelUnlocked()
    {
        return PlayerPrefs.GetInt("MaxLevelUnlocked", 0);
    }
}