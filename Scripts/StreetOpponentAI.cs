using UnityEngine;

public class StreetOpponentAI : MonoBehaviour
{
    public Transform target;
    public float runSpeed = 4.5f;
    public float stopDistance = 1.4f;

    void Update()
    {
        if (!target) return;
        Vector3 delta = target.position - transform.position;
        delta.y = 0f;
        if (delta.magnitude <= stopDistance) return;

        Vector3 direction = delta.normalized;
        transform.position += direction * runSpeed * Time.deltaTime;
        if (direction.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), 8f * Time.deltaTime);
    }
}
