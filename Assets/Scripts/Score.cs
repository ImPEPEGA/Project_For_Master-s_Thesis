using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    private float _score = 0f;
    private Rigidbody lastRb;
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb != lastRb)
        {
            lastRb = rb;
            AddScore();
        }
        else
        {
            return;
        }
    }
    void AddScore()
    {
        _score += 10f;
        // Settings.Instance.score += _score;
        // PlayerPrefs.SetFloat("PlayerScore", Settings.Instance.score);
        // PlayerPrefs.Save();

        _scoreText.text = _score.ToString();
    }
}
