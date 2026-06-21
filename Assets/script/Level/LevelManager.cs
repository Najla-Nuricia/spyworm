using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [System.Serializable]
    public class Level
    {
        public GameObject levelObject;
    }

    public Level[] levels;
    public int currentLevel = 0;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        // matikan semua level dulu
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].levelObject.SetActive(false);
        }

        // aktifkan level sekarang
        levels[currentLevel].levelObject.SetActive(true);
    }

    public void NextLevel()
    {
        levels[currentLevel].levelObject.SetActive(false);
        
        currentLevel++;

        if (currentLevel >= levels.Length)
        {
            currentLevel = levels.Length - 1; 

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            return; 
        }

        levels[currentLevel].levelObject.SetActive(true);
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}