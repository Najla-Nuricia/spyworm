using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Pengaturan Jeda Scene (Detik)")]
    [SerializeField] private float delayTime = 0.5f; 

    // Tempat menitipkan data angka level agar tidak hilang saat scene dimuat ulang
    [HideInInspector] public int savedLevel = 0;
    [HideInInspector] public int totalLevel;

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
        // Saat kembali ke Main Menu, reset data level kembali ke nol
        savedLevel = 0;
        StartCoroutine(LoadSceneWithDelay("MainMenu"));
    }

    public void startGame()
    {
        savedLevel = 0;
        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }

    public void GoToNextLevel()
    {
        savedLevel++;

        if (savedLevel >= totalLevel)
        {
            savedLevel = 0;
            StartCoroutine(LoadSceneWithDelay("MainMenu"));
            return;
        }

        StartCoroutine(LoadSceneWithDelay("Gameplay"));
    }


    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }
}