using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject completePanel;

    private void Start()
    {
        gameOverPanel.SetActive(!GameState.IsWin);
        completePanel.SetActive(GameState.IsWin);
    }
}