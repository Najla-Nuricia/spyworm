using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Wajib ditambahkan untuk menggunakan Coroutine

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pengaturan Jeda Scene (Detik)")]
    [SerializeField] private float delayTime = 0.5f; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        StartCoroutine(LoadSceneWithDelay("Result"));
    }

    public void Restart()
    {
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void Home()
    {
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void startGame()
    {
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }
}