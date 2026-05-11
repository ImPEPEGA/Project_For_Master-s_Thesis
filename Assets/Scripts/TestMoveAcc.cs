using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Globalization;

public class TestMoveAcc : MonoBehaviour
{
    SerialPort sp;
    public string portName = "COM6";
    public int baudRate = 9600;
    void Start()
    {
            sp = new SerialPort(portName, baudRate);
            sp.Open();
            sp.ReadTimeout = 100;
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            sphereMove();
        }
    }

    void sphereMove()
    {
        string accData = sp.ReadLine();
        float[] parsedData = parseData(accData);
        if (parsedData != null && parsedData.Length == 2)
        {
            Debug.Log($"X: {parsedData[0]}, Y: {parsedData[1]}");
        }
    }

    float[] parseData(string data)
    {
        float x = 0;
        float y = 0;
        string[] parts = data.Split(',');
        if (parts.Length != 2)
        {
            return null;
        }
            x = float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
            y = float.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);

        return new float[] { x, y };
    }
    
    void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen) {
            sp.Close();
        }
    }
}
