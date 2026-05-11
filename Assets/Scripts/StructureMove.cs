using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float _structureSpeed = 10f;
    void Update()
    {
        DestroyStructure();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(-transform.forward * _structureSpeed * 4 * Time.fixedDeltaTime);
    }

    private void DestroyStructure()
    {
        if (transform.position.z < -30f) {
            Destroy(gameObject);
        }
    }
}
