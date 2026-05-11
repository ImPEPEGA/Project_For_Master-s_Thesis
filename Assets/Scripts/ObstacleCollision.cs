using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalDistanceText;
    public AudioSource audioSource;
    public AudioClip crashSound;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(crashSound);
            rb.useGravity = true;
        }
        if (collision.gameObject.tag == "Floor")
        {
            
            Settings.Instance.score = PlayerPrefs.GetFloat("PlayerScore", 0f);
            Settings.Instance.score += float.Parse(finalScoreText.text);
            PlayerPrefs.SetFloat("PlayerScore", Settings.Instance.score);
            PlayerPrefs.Save();
            Debug.Log("Final Score: " + Settings.Instance.score);

            // try
            // {
                if (float.Parse(finalScoreText.text) > PlayerPrefs.GetFloat("MaxPlayerScore", 0))
                {
                    PlayerPrefs.SetFloat("MaxPlayerScore", float.Parse(finalScoreText.text));
                    PlayerPrefs.Save();
                }
            // }
            // catch (Exception ex)
            // {
            //     Debug.Log(ex.Message);
            // }

            // try
            // {
                if (float.Parse(finalDistanceText.text) > PlayerPrefs.GetFloat("MaxPlayerDistance", 0))
                {
                    PlayerPrefs.SetFloat("MaxPlayerDistance", float.Parse(finalDistanceText.text));
                    PlayerPrefs.Save();
                }
            // }
            // catch (Exception ex)
            // {
            //     Debug.Log(ex.Message);
            // }

            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
