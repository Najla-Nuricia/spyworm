using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Button retryButton; 

    private void Start()
    {
        if (retryButton != null && GameManager.Instance != null)
        {
            retryButton.onClick.RemoveAllListeners(); 
            retryButton.onClick.AddListener(GameManager.Instance.Restart); 
        }
        else
        {
            Debug.LogWarning("Retry Button atau GameManager.Instance tidak ditemukan di Gameplay!");
        }
    }
}