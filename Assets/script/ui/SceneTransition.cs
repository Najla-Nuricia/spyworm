using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class SceneTransition : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 2f;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
    }

    void Start()
    {
        canvasGroup.DOFade(0, fadeDuration);
    }

    public void StartTransition(string sceneName)
    {
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOKill();

        canvasGroup
            .DOFade(1, fadeDuration)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName);
            });
    }

    private void OnDestroy()
    {
        canvasGroup.DOKill();
    }
}