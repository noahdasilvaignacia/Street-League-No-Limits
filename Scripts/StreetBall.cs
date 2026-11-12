using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StreetBall : MonoBehaviour
{
    public float throwForce = 13f;
    public float upwardForce = 2.5f;
    public Transform handOrThrowPoint;
    Rigidbody body;
    bool held = true;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
    }

    public void ThrowBall()
    {
        if (!held) return;
        held = false;
        transform.SetParent(null);
        body.isKinematic = false;
        Vector3 originForward = handOrThrowPoint ? handOrThrowPoint.forward : Camera.main.transform.forward;
        body.AddForce(originForward.normalized * throwForce + Vector3.up * upwardForce, ForceMode.VelocityChange);
    }

    public void AttachTo(Transform hand)
    {
        held = true;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        body.isKinematic = true;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public bool IsHeld() => held;
}
