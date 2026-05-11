using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
    }

    public void RestartHandler()
    {
        // PauseMenu.Instance.RestartMenuHandler();
        // Settings.Instance.audioSource.PlayOneShot(Settings.Instance.buttonClickSound);
        // StartCoroutine(PlaySoundAndWait());

        audioSource.PlayOneShot(buttonClickSound);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }
    public void ReturnMainMenuHandler()
    {
        // PauseMenu.Instance.ReturnMainMenuHandler();
        // Settings.Instance.audioSource.PlayOneShot(Settings.Instance.buttonClickSound);
        // StartCoroutine(PlaySoundAndWait());

        audioSource.PlayOneShot(buttonClickSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
