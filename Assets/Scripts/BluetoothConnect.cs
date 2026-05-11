using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Application = UnityEngine.Application;

public class BluetoothConnect : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip buttonClickSound;
    public Text deviceMACadd;
    public Text connectionResult;
    public Text receivedDataText;
    public string receivedData;
    private bool isConnected;

    private static AndroidJavaClass unity3dbluetoothplugin;
    private static AndroidJavaObject BluetoothConnector;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = Settings.Instance.audioSource;
        buttonClickSound = Settings.Instance.buttonClickSound;
        InitBluetooth();
        isConnected = false;
        connectionResult.color = Color.red;
        connectionResult.text = "Controller not connected";
    }

    public void InitBluetooth()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        // Check BT and location permissions
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
            || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADMIN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
        {

            Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_ADMIN",
                            "android.permission.BLUETOOTH",
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });

        }

        unity3dbluetoothplugin = new AndroidJavaClass("com.example.unity3dbluetoothplugin.BluetoothConnector");
        BluetoothConnector = unity3dbluetoothplugin.CallStatic<AndroidJavaObject>("getInstance");
    }

    public void StartConnection()
    {
        audioSource.PlayOneShot(buttonClickSound);
        if (Application.platform != RuntimePlatform.Android)
        {
            return;
        }

        BluetoothConnector.CallStatic("StartConnection", deviceMACadd.text.ToString().ToUpper());
        if (isConnected)
        {
            connectionResult.text = "Connected";
        }
        else
        {
            connectionResult.text = "Not connected";
        }
    }

    public void ConnectionStatus(string status)
    {
        Toast("Connection Status: " + status);
        isConnected = status == "Connected";
        // if (isConnected)
        // {
        //     connectionResult.text = "Connected";
        // }
        // else
        // {
        //     connectionResult.text = "Not connected";
        // }
    }

    public void Toast(string data)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("Toast", data);
    }

    public void ReadData(string data)
    {
        Debug.Log("BT Stream: " + data);
        receivedDataText.text = data;
        if (data is null) {
            receivedDataText.text = "Data is null";
        }
    }
    public string ReceivedData()
    {
        return receivedData;
    }
}
