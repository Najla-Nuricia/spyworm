using UnityEngine;

public class WormDragAudio : MonoBehaviour
{
    [Header("Pengaturan Audio Drag")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dragSound;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = dragSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        if (dragSound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(dragSound);
        }
    }

    void OnMouseUp()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}