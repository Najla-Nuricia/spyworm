using UnityEngine;
using DG.Tweening;

public class ObstacleMovement : MonoBehaviour
{
    public enum MovementType { Linear, Rotation }

    [Header("Tipe Gerakan")]
    [SerializeField] private MovementType movementType = MovementType.Linear;

    [Header("Pengaturan Gerak Lurus (Linear)")]
    [SerializeField] private Vector3 moveTarget;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private Ease moveEase = Ease.InOutSine;

    [Header("Pengaturan Putaran (Rotation)")]
    [SerializeField] private Vector3 rotateTarget = new Vector3(0, 0, 90);
    [SerializeField] private float rotateDuration = 3f;
    [SerializeField] private Ease rotateEase = Ease.Linear;

    private void Start()
    {
        if (movementType == MovementType.Linear)
        {
            transform.DOLocalMove(transform.localPosition + moveTarget, moveDuration)
                .SetEase(moveEase)
                .SetLoops(-1, LoopType.Yoyo);
        }
        else if (movementType == MovementType.Rotation)
        {
            transform.DOLocalRotate(transform.localEulerAngles + rotateTarget, rotateDuration, RotateMode.FastBeyond360)
                .SetEase(rotateEase)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}