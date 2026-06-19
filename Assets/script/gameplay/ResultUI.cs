using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject completePanel;

    [Header("Tombol Navigasi Utama")]
    [SerializeField] private Button retryButton; 
    [SerializeField] private Button nextLevelButton;

    [Header("List Tombol Home")]
    [SerializeField] private List<Button> homeButtons = new List<Button>(); 

    private void Start()
    {
        gameOverPanel.SetActive(!GameState.IsWin);
        completePanel.SetActive(GameState.IsWin);

        // 1. Menghubungkan tombol Retry tunggal ke GameManager
        if (retryButton != null && GameManager.Instance != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(GameManager.Instance.Restart);
        }

        // 2. Menghubungkan semua tombol Home yang ada di dalam List ke GameManager
        if (GameManager.Instance != null)
        {
            foreach (Button btn in homeButtons)
            {
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(GameManager.Instance.Home);
                }
            }
        }

        // 3. Menghubungkan tombol Next Level ke LevelManager
        if (nextLevelButton != null && LevelManager.Instance != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(LevelManager.Instance.NextLevel);
        }
    }
}