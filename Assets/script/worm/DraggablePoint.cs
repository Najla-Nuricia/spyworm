using UnityEngine;
using UnityEngine.InputSystem;

public class DraggablePoint : MonoBehaviour
{
    [SerializeField] private BoxCollider2D ground;
    [SerializeField] private float groundOffset = 0.2f;

    private bool dragging;
    private Collider2D myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos =
                Camera.main.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );

            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit == myCollider)
            {
                dragging = true;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
        }

        if (dragging)
        {
            Vector3 pos =
                Camera.main.ScreenToWorldPoint(
                    Mouse.current.position.ReadValue()
                );

            pos.z = 0;

            float groundTop =
                ground.bounds.max.y + groundOffset;

            // Jangan masuk tanah
            if (pos.y < groundTop)
            {
                pos.y = groundTop;
            }

            Vector3 bottomLeft =
                Camera.main.ViewportToWorldPoint(
                    new Vector3(0, 0, 0)
                );

            Vector3 topRight =
                Camera.main.ViewportToWorldPoint(
                    new Vector3(1, 1, 0)
                );

            float margin = 0.3f;

            CircleCollider2D circle =
                GetComponent<CircleCollider2D>();

            if (circle != null)
            {
                margin = circle.radius;
            }

            pos.x = Mathf.Clamp(
                pos.x,
                bottomLeft.x + margin,
                topRight.x - margin
            );

            pos.y = Mathf.Clamp(
                pos.y,
                groundTop,
                topRight.y - margin
            );

            transform.position = pos;
        }
    }
}