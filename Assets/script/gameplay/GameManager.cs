using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pengaturan Jeda Scene (Detik)")]
    [SerializeField] private float delayTime = 0.5f; 

    [Header("Pengaturan Durasi Komik (Detik)")]
    [SerializeField] private float comicDuration = 4f;

    [Header("Pengaturan Durasi Fade (Detik)")]
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Reference Panel UI Terpusat")]
    public CanvasGroup globalComicPanel;
    public CanvasGroup globalLevelSelectPanel;

    [HideInInspector] public int savedLevel = 0;
    [HideInInspector] public int totalLevel;
    [HideInInspector] public bool isGameBeaten = false;

    void Awake()
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

    public void PlayLevel1WithComic()
    {
        StartCoroutine(Level1ComicSequence());
    }

    private IEnumerator Level1ComicSequence()
    {
        if (globalLevelSelectPanel != null)
        {
            globalLevelSelectPanel.DOFade(0f, fadeDuration);
            globalLevelSelectPanel.interactable = false;
            globalLevelSelectPanel.blocksRaycasts = false;
            yield return new WaitForSeconds(fadeDuration);
            globalLevelSelectPanel.gameObject.SetActive(false);
        }

        if (globalComicPanel != null)
        {
            // Paksa aktifkan game object fisiknya dulu biar ga ghaib
            globalComicPanel.gameObject.SetActive(true); 
            globalComicPanel.alpha = 0f;
            globalComicPanel.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }

        yield return new WaitForSeconds(comicDuration);

        if (globalComicPanel != null)
        {
            globalComicPanel.DOFade(0f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
            globalComicPanel.gameObject.SetActive(false);
        }

        savedLevel = 0;
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

    public void startGame()
    {
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void GoToNextLevel()
    {
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

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }

    public void UnlockNextLevel(int completedLevelIndex)
    {
        int nextLevel = completedLevelIndex + 1;

        if (nextLevel >= totalLevel) 
        {
            return;
        }

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