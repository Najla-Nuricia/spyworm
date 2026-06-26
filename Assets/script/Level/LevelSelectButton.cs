using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private int targetLevelIndex;
    private Button btn;
    private Image[] allImages;

    void Awake()
    {
        btn = GetComponent<Button>();
        allImages = GetComponentsInChildren<Image>();
        
        btn.onClick.RemoveListener(SelectLevel);
        btn.onClick.AddListener(SelectLevel);
    }

    void OnEnable()
    {
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (allImages == null || allImages.Length == 0) allImages = GetComponentsInChildren<Image>();

        // Level 1 selalu terbuka, level lain cek progres
        int maxUnlocked = GameManager.Instance != null ? GameManager.Instance.GetMaxLevelUnlocked() : 0;
        bool isUnlocked = (targetLevelIndex == 0 || targetLevelIndex <= maxUnlocked);

        btn.interactable = isUnlocked;

        if (isUnlocked)
        {
            SetImagesColorAndMaterial(Color.white, null);
        }
        else
        {
            SetImagesColorAndMaterial(new Color(0.5f, 0.5f, 0.5f, 0.8f), Canvas.GetDefaultCanvasMaterial());
        }
    }

    private void SetImagesColorAndMaterial(Color targetColor, Material targetMaterial)
    {
        for (int i = 0; i < allImages.Length; i++)
        {
            if (allImages[i] != null)
            {
                allImages[i].material = targetMaterial;
                allImages[i].color = targetColor;
            }
        }
    }

    void SelectLevel()
    {
        if (GameManager.Instance == null) return;

        // KUNCI UTAMA: Level 1 selalu masuk ke komik, tidak peduli game sudah tamat atau belum!
        if (targetLevelIndex == 0)
        {
            GameManager.Instance.PlayLevel1WithComic();
        }
        else
        {
            GameManager.Instance.savedLevel = targetLevelIndex;
            GameManager.Instance.startGame();
        }
    }
}