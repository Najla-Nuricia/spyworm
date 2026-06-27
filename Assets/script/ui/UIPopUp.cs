using UnityEngine;
using DG.Tweening;

public class UIPopUp : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease openEase = Ease.OutBack;
    [SerializeField] private Ease closeEase = Ease.InBack;
    
    [Header("Target Level (0 = Level 1)")]
    [SerializeField] private int targetLevelIndex = 0; 

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Start()
    {
        // Mengecek apakah LevelManager sudah ada dan membandingkan level saat ini
        if (LevelManager.Instance != null)
        {
            if (LevelManager.Instance.currentLevel == targetLevelIndex)
            {
                PopUp();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void PopUp()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        
        transform.DOScale(originalScale, duration)
            .SetEase(openEase)
            .SetUpdate(true); 
    }

    public void PopDown()
    {
        transform.DOScale(Vector3.zero, duration)
            .SetEase(closeEase)
            .SetUpdate(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
}