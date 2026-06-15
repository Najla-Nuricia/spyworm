using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WormController : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public int segments = 20;
    public float curveHeight = 2f;

    private LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments;
    }

    void Update()
    {
        DrawWorm();
    }

    void DrawWorm()
    {
        Vector3 center = (head.position + tail.position) / 2f;

        float distance =
            Vector3.Distance(head.position, tail.position);

        Vector3 perp =
            Vector3.Cross(
                (tail.position - head.position).normalized,
                Vector3.forward
            );

        Vector3 controlPoint =
            center + perp * curveHeight * Mathf.Clamp01(1f - distance / 5f);

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);

            Vector3 p =
                Mathf.Pow(1 - t, 2) * head.position +
                2 * (1 - t) * t * controlPoint +
                Mathf.Pow(t, 2) * tail.position;

            lr.SetPosition(i, p);
        }
    }
}