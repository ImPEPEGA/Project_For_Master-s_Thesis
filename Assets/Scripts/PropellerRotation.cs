using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRotation : MonoBehaviour
{
    private float _speedPropeller = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100 * _speedPropeller * Time.deltaTime);
    }
}
