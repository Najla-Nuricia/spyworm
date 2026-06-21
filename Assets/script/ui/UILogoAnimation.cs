using UnityEngine;
using DG.Tweening;

public class UILogoAnimation : MonoBehaviour
{
    [Header("Pengaturan Ayunan (Swing)")]
    [SerializeField] private float swingAngle = 15f;
    [SerializeField] private float swingSpeed = 2f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, -swingAngle);

        transform.DOLocalRotate(new Vector3(0, 0, swingAngle), swingSpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}