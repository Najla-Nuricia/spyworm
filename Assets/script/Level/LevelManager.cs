using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [System.Serializable]
    public class Level
    {
        public GameObject levelObject;
    }

    public Level[] levels;
    
    public int currentLevel 
    {
        get { return GameManager.Instance != null ? GameManager.Instance.savedLevel : 0; }
        set { if (GameManager.Instance != null) GameManager.Instance.savedLevel = value; }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.totalLevel = levels.Length;
        }

        LoadLevel();
    }

    public void LoadLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].levelObject != null)
            {
                levels[i].levelObject.SetActive(false);
            }
        }

        if (currentLevel >= 0 && currentLevel < levels.Length)
        {
            levels[currentLevel].levelObject.SetActive(true);
        }
    }

    public void NextLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UnlockNextLevel(currentLevel);
        }

        currentLevel++;

        if (currentLevel >= levels.Length)
        {
            currentLevel = levels.Length - 1;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            LoadLevel();
        }
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}