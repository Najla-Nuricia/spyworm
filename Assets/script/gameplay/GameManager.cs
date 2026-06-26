using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float delayTime = 0.5f;

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
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void GoToNextLevel()
    {
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
        SceneManager.LoadScene(sceneName);
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