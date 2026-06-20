using UnityEngine;
using DG.Tweening;

public class GameMusicManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip musicClip;
    [Range(0f, 1f)] [SerializeField] private float volume = 1f;
    [SerializeField] private float fadeDuration = 0.5f;

    private AudioSource audioSource;
    private Tween fadeTween;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            
            audioSource.volume = 0f;
            audioSource.Play();
            
            fadeTween = audioSource.DOFade(volume, fadeDuration).SetUpdate(true);
        }
    }

    public void FadeOutAndStop()
    {
        if (audioSource == null) return;

        if (fadeTween != null && fadeTween.IsActive())
        {
            fadeTween.Kill();
        }

        audioSource.DOFade(0f, fadeDuration)
            .SetUpdate(true)
            .OnComplete(() => audioSource.Stop());
    }
}