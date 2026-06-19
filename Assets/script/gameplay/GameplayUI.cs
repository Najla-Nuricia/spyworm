using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button retryButton; 
    [SerializeField] private Button homeButton;
    [SerializeField] private Button startGameButton;

    private void Start()
    {
        if (retryButton != null && GameManager.Instance != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(GameManager.Instance.Restart);
        }

        if (homeButton != null && GameManager.Instance != null)
        {
            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(GameManager.Instance.Home);
        }

        if (startGameButton != null && GameManager.Instance != null)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(GameManager.Instance.startGame);
        }
    }
}