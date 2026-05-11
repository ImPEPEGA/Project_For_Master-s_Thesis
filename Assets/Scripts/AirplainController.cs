 using System.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.IO.Ports;
using System.Globalization;
using UnityEngine.UI;

public class AirplainController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private float _speedRotation = 0.05f;
    private float _speedPositionController = 10f;
    private float _speedPositionMobile = 0.4f;
    private float[] _maxRotationZ = { -0.5f, 0.5f };
    private float[] _maxRotationX = { -0.5f, 0.5f };
    private float[] _readyDataController;
    private float calibrationX = 0f;
    private float calibrationY = 0f;
    private Gyroscope _gyro;
    private Rigidbody rb;
    public Text _receivedData;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _checkGyroActivity();
        CalibrateGyro();
        // _startPosition = _gyro.attitude.eulerAngles;
        // Debug.Log(_startPosition);
    }
    void Update()
    {
        _readyDataController = ParseData();

        airplaneRotation(_readyDataController);

        airplanePosition(_readyDataController);

        airplaneStabilization();

        DestroyPlayer();
    }

    void _checkGyroActivity()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _gyro = Input.gyro;
            _gyro.enabled = true;
        }
    }

    void CalibrateGyro()
    {
        Quaternion deviceRot = _gyro.attitude;
        var rot = Quaternion.Euler(90f, 0f, 0f) * 
                new Quaternion(-deviceRot.x, -deviceRot.y, deviceRot.z, deviceRot.w);

        calibrationX = NormalizeAngle(rot.eulerAngles.x);
        calibrationY = NormalizeAngle(rot.eulerAngles.z);
        Debug.Log($"Calibration X: {calibrationX}, Y: {calibrationY}");
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    void airplaneRotation(float[] readyData, bool _mobileControl = true)
    {
        if (!_mobileControl)
        {
            var rotationAirplane = transform.rotation;
            // float verSpeed = readyData[0];
            // float horSpeed = readyData[1];

            float mapX = Map(readyData[0], -10f, 10f, -0.5f, 0.5f);
            float mapY = Map(readyData[1], -10f, 10f, -0.5f, 0.5f);

            // rotationAirplane.z += mapY * _speedRotation;
            // rotationAirplane.x += mapX * _speedRotation;

            // transform.rotation = rotationAirplane;
            transform.Rotate(mapX * _speedRotation, 0f, mapY * _speedRotation);
        } else
        {
            Quaternion deviseRotation = _gyro.attitude;
            var rotationAirplane = Quaternion.Euler(90f, 0f, 0f) * (new Quaternion(-deviseRotation.x, -deviseRotation.y, deviseRotation.z, deviseRotation.w));

            // Debug.Log($"X: {rotationAirplane.eulerAngles.x}, Z: {rotationAirplane.eulerAngles.z}");
            // Debug.Log($"Raw X: {deviseRotation.x}, Raw Y: {deviseRotation.z}");

            // float tiltX = rotationAirplane.eulerAngles.x;
            // float tiltY = rotationAirplane.eulerAngles.z;
            // // Debug.Log($"Tilt X: {tiltX}, Tilt Y: {tiltY}");

            // if (tiltX > 180) tiltX -= 360;
            // if (tiltY > 180) tiltY -= 360;

            float tiltX =
                NormalizeAngle(rotationAirplane.eulerAngles.x) - calibrationX;
            float tiltY =
                NormalizeAngle(rotationAirplane.eulerAngles.z) - calibrationY;

            // Debug.Log($"Tilt X: {tiltX}, Tilt Y: {tiltY}");

            transform.Rotate(tiltX * _speedRotation, 0f, tiltY * _speedRotation);
        }
    }
    void airplanePosition(float[] readyData, bool _mobileControl = true)
    {
        if (!_mobileControl) 
        {
            float[] parsedData = readyData;
            if (parsedData != null && parsedData.Length == 2)
            {
                float mapX = Map(parsedData[0], -10f, 10f, -2f, 2f);
                float mapY = Map(parsedData[1], -10f, 10f, -2f, 2f);
                // Debug.Log("X: " + mapX + " Y: " + mapY);
                Vector3 movement = new Vector3(-mapY, -mapX, 0.0f);
                rb.AddForce(movement * _speedPositionController);
            }
        } else
        {
            Quaternion deviseRotation = _gyro.attitude;
            var rotationAirplane = Quaternion.Euler(90f, 0f, 0f) * (new Quaternion(-deviseRotation.x, -deviseRotation.y, deviseRotation.z, deviseRotation.w));

            // float tiltX = rotationAirplane.eulerAngles.x;
            // float tiltY = rotationAirplane.eulerAngles.z;

            // if (tiltX > 180) tiltX -= 360;
            // if (tiltY > 180) tiltY -= 360;

            float tiltX =
                NormalizeAngle(rotationAirplane.eulerAngles.x) - calibrationX;
            float tiltY =
                NormalizeAngle(rotationAirplane.eulerAngles.z) - calibrationY;

            Vector3 movement = new Vector3(-tiltY, -tiltX, 0.0f);
            rb.AddForce(movement * _speedPositionMobile);
        }
    }
    void airplaneStabilization()
    {
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 2f);

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 1.5f);
    }
    float[] ParseData()
    {
        string data = _receivedData.text;
        try
        {
            string[] parts = data.Split(',');
            if (parts.Length != 2) return null;

            float x = float.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
            float y = float.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);

            return new float[] { x, y };
        }
        catch
        {
            return null;
        }
    }
    public static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
    }
    private void DestroyPlayer()
    {
        if (transform.position.z < -12f)
        {
            Destroy(gameObject);
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
