using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.IsWin = false;
            SceneManager.LoadScene(sceneName);
        }
    }
}