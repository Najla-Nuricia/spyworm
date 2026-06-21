using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WormController : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public int segments = 20;
    public float curveHeight = 2f;
    public float maxBodyLength = 4f;

    private LineRenderer lr;
    private Vector3 lastHeadPosition;
    private Vector3 lastTailPosition;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments;

        if (head != null) lastHeadPosition = head.position;
        if (tail != null) lastTailPosition = tail.position;
    }

    void Update()
    {
        ConstrainDistance();
        DrawWorm();
    }

    void ConstrainDistance()
    {
        if (head == null || tail == null) return;

        float currentDistance = Vector3.Distance(head.position, tail.position);

        if (currentDistance > maxBodyLength)
        {
            bool headMoved = head.position != lastHeadPosition;
            bool tailMoved = tail.position != lastTailPosition;

            if (headMoved && !tailMoved)
            {
                Vector3 directionToTail = (tail.position - head.position).normalized;
                tail.position = head.position + directionToTail * maxBodyLength;
            }
            else if (tailMoved && !headMoved)
            {
                Vector3 directionToHead = (head.position - tail.position).normalized;
                head.position = tail.position + directionToHead * maxBodyLength;
            }
            else if (headMoved && tailMoved)
            {
                Vector3 center = (head.position + tail.position) / 2f;
                Vector3 directionToHead = (head.position - tail.position).normalized;
                head.position = center + directionToHead * (maxBodyLength / 2f);
                tail.position = center - directionToHead * (maxBodyLength / 2f);
            }
        }

        lastHeadPosition = head.position;
        lastTailPosition = tail.position;
    }

    void DrawWorm()
    {
        Vector3 center = (head.position + tail.position) / 2f;
        float distance = Vector3.Distance(head.position, tail.position);

        Vector3 perp = Vector3.Cross(
            (tail.position - head.position).normalized,
            Vector3.forward
        );

        Vector3 controlPoint = center + perp * curveHeight * Mathf.Clamp01(1f - distance / maxBodyLength);

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