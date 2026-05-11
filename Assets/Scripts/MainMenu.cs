using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    public string _connectionStatus;

    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
    }
    public void StartGame()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(1);
        //CONNECTED TO BLUETOOTH DEVICE
        /*
        _connectionStatus = BluetoothManager.isConnected.ToString();
        if (_connectionStatus.Length == 0 || _connectionStatus == "False")
        {
            SceneManager.LoadScene(2);
        }
        else if (_connectionStatus == "True")
        {
            SceneManager.LoadScene(1);
        }
        */
    }
    public void ShopMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(3);
    }
    public void InventoryMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(4);
    }
    public void StatisticsMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(5);
    }
    public void BluetoothMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(2);
    }
    public void BackMainMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene(0);
    }
    public void QuitHandler()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Application.Quit();
    }
}
