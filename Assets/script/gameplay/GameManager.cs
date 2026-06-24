using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening; // Wajib ditambahkan untuk DOTween

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pengaturan Jeda Scene (Detik)")]
    [SerializeField] private float delayTime = 0.5f; 

    [Header("Pengaturan Durasi Komik (Detik)")]
    [SerializeField] private float comicDuration = 4f;

    [Header("Pengaturan Durasi Fade (Detik)")]
    [SerializeField] private float fadeDuration = 0.5f;

    [HideInInspector] public int savedLevel = 0;
    [HideInInspector] public int totalLevel;

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

    public void PlayGameWithComic(CanvasGroup comicCG, CanvasGroup mainMenuCG)
    {
        StartCoroutine(ComicSequence(comicCG, mainMenuCG));
    }

    private IEnumerator ComicSequence(CanvasGroup comicCG, CanvasGroup mainMenuCG)
    {
        // 1. Fade Out Menu Utama
        if (mainMenuCG != null)
        {
            mainMenuCG.DOFade(0f, fadeDuration);
            mainMenuCG.interactable = false;
            mainMenuCG.blocksRaycasts = false;
            yield return new WaitForSeconds(fadeDuration);
            mainMenuCG.gameObject.SetActive(false);
        }

        // 2. Fade In Panel Komik
        if (comicCG != null)
        {
            comicCG.gameObject.SetActive(true);
            comicCG.alpha = 0f;
            comicCG.DOFade(1f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }

        // 3. Tahan Komik Sesuai Durasi
        yield return new WaitForSeconds(comicDuration);

        // 4. Fade Out Panel Komik Sebelum Pindah Scene
        if (comicCG != null)
        {
            comicCG.DOFade(0f, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }

        // 5. Pindah ke Scene Gameplay
        SceneManager.LoadScene("Gameplay");
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
        savedLevel = 0;
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void startGame()
    {
        savedLevel = 0;
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void GoToNextLevel()
    {
        savedLevel++;

        if (savedLevel >= totalLevel)
        {
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
}