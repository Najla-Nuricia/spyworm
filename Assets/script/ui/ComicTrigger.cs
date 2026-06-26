using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComicTrigger : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup levelSelectPanel;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Start()
    {
        if (levelSelectPanel != null)
        {
            levelSelectPanel.alpha = 0f;
            levelSelectPanel.gameObject.SetActive(false);
        }

        if (playButton != null)
        {
            playButton.onClick.RemoveListener(OpenLevelSelection);
            playButton.onClick.AddListener(OpenLevelSelection);
        }
    }

    private void OpenLevelSelection()
    {
        if (mainMenuPanel != null && levelSelectPanel != null)
        {
            mainMenuPanel.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                mainMenuPanel.gameObject.SetActive(false);

                levelSelectPanel.gameObject.SetActive(true);
                levelSelectPanel.alpha = 0f;
                levelSelectPanel.DOFade(1f, fadeDuration);
                levelSelectPanel.interactable = true;
                levelSelectPanel.blocksRaycasts = true;
            });
        }
    }
}