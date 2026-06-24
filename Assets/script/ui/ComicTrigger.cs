using UnityEngine;
using UnityEngine.UI;

public class ComicTrigger : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private CanvasGroup comicPanel;

    private void Start()
    {
        if (comicPanel != null)
        {
            comicPanel.alpha = 0f;
            comicPanel.gameObject.SetActive(false);
        }

        if (playButton != null && GameManager.Instance != null)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(TriggerComicSequence);
        }
    }

    private void TriggerComicSequence()
    {
        if (mainMenuPanel != null && comicPanel != null)
        {
            GameManager.Instance.PlayGameWithComic(comicPanel, mainMenuPanel);
        }
    }
}