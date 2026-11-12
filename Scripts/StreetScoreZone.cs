using UnityEngine;

public class StreetScoreZone : MonoBehaviour
{
    public int points = 6;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<StreetBall>() != null)
        {
            StreetGameManager manager = FindObjectOfType<StreetGameManager>();
            if (manager) manager.AddTouchdown(points);
        }
    }
}
