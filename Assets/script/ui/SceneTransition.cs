using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class SceneTransition : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // Pastikan saat game mulai, layar transparan
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    // Panggil fungsi ini dari mana saja (tombol, trigger, dll)
    public void StartTransition(string sceneName)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    // Panggil fungsi ini lewat event sceneLoaded agar layar terbuka otomatis saat scene baru siap
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, fadeDuration);
        canvasGroup.blocksRaycasts = false;
    }
}