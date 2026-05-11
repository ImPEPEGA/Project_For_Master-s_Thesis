using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private StructureSpawner _structureSpawner;
    private Rigidbody lastRb;
    public int countSpawnTrigger = 0;

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb != lastRb)
        {
            lastRb = rb;
            if (countSpawnTrigger >= 500)
            {
                _structureSpawner.ChooseBiome(countSpawnTrigger);
                countSpawnTrigger = 0;
            } else
            {
                countSpawnTrigger += 100;
            }
            Debug.Log(countSpawnTrigger);
            _structureSpawner.Spawn();
            Debug.Log(other.name);
        }
        else
        {
            return;
        }
    }
}
