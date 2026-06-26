using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ComicManager : MonoBehaviour
{
    public static ComicManager Instance;

    [SerializeField] private CanvasGroup comicPanel;
    [SerializeField] private CanvasGroup levelSelectPanel;

    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float comicDuration = 4f;

    private void Awake()
    {
        Instance = this;

        comicPanel.alpha = 0;
        comicPanel.gameObject.SetActive(false);
    }

    public void PlayComic()
    {
        StartCoroutine(ComicSequence());
    }

    IEnumerator ComicSequence()
    {
        levelSelectPanel.DOFade(0, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        levelSelectPanel.gameObject.SetActive(false);

        comicPanel.gameObject.SetActive(true);
        comicPanel.alpha = 0;

        comicPanel.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        yield return new WaitForSeconds(comicDuration);

        comicPanel.DOFade(0, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        comicPanel.gameObject.SetActive(false);

        GameManager.Instance.savedLevel = 0;
        GameManager.Instance.StartGame();
    }
}