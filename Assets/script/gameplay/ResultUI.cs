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

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    private void Start()
    {
        gameOverPanel.SetActive(!GameState.IsWin);
        completePanel.SetActive(GameState.IsWin);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (GameState.IsWin && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }
        else if (!GameState.IsWin && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }

        if (retryButton != null && GameManager.Instance != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(GameManager.Instance.Restart);
        }

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

        if (nextLevelButton != null && LevelManager.Instance != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(LevelManager.Instance.NextLevel);
        }
    }
}