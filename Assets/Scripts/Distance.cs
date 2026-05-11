using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _distanceText;
    private float _distance = 0f;
    private Rigidbody lastRb;
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb != lastRb)
        {
            lastRb = rb;
            AddDistance();
        }
        else
        {
            return;
        }
    }

    void AddDistance()
    {
        _distance += 100f;
        _distanceText.text = _distance.ToString();
    }
}
