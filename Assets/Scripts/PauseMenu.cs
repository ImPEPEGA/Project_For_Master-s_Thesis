using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    [SerializeField] GameObject pauseCanvas;

    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
    }
    public void ContinueHandler()
    {
        // StartCoroutine(PlaySoundAndWait());
        audioSource.PlayOneShot(buttonClickSound);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseMenuHandler()
    {
        // StartCoroutine(PlaySoundAndWait());
        audioSource.PlayOneShot(buttonClickSound);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartMenuHandler()
    {
        // StartCoroutine(PlaySoundAndWait());
        audioSource.PlayOneShot(buttonClickSound);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }

    public void ReturnMainMenuHandler()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
