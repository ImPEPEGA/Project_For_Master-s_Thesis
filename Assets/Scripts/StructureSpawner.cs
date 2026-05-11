using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private List<GameObject> _obstaclesJungle;
    [SerializeField] private List<GameObject> _obstaclesSkyscrapers;
    [SerializeField] private float _lengthPlatformObstacles = 60f;
    private GameObject _obstacle;
    private GameObject _obstacleJungle;
    private GameObject _obstacleSkyscrapers;
    private int randomBiome;
    private Vector3 position;

    private GameObject _currentObstacle;
    // private int countSpawnTrigger = 0;
    void Start()
    {
        SpawnObstacle(_obstacles);
        // _obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Count - 1)],
        //     transform.position, Quaternion.identity);
        // _obstacleJungle = Instantiate(_obstaclesJungle[Random.Range(0, _obstaclesJungle.Count - 1)],
        //     transform.position, Quaternion.identity);
        // _obstacleSkyscrapers = Instantiate(_obstaclesJungle[Random.Range(0, _obstaclesJungle.Count - 1)],
        //     transform.position, Quaternion.identity);
    }

    public void ChooseBiome(int countSpawnTrigger)
    {
        if (countSpawnTrigger >= 500)
        {
            randomBiome = Random.Range(0, 3);
        }
    }

    public void Spawn()
    {
        switch (randomBiome)
        {
            case 0:
                SpawnObstacle(_obstacles);
                Debug.Log("Desert");
                // position = new Vector3(0, 0, _obstacle.transform.position.z + _lengthPlatformObstacles);
                // _obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Count - 1)],
                //     position, Quaternion.identity);
                break;
            case 1:
                SpawnObstacle(_obstaclesJungle);
                Debug.Log("Jungle");
                // position = new Vector3(0, 0, _obstacleJungle.transform.position.z + _lengthPlatformObstacles);
                // _obstacleJungle = Instantiate(_obstaclesJungle[Random.Range(0, _obstaclesJungle.Count - 1)],
                //     position, Quaternion.identity);
                break;
            case 2:
                SpawnObstacle(_obstaclesSkyscrapers);
                Debug.Log("Skyscrapers");
                // position = new Vector3(0, 0, _obstacleSkyscrapers.transform.position.z + _lengthPlatformObstacles);
                // _obstacleSkyscrapers = Instantiate(_obstaclesSkyscrapers[Random.Range(0, _obstaclesSkyscrapers.Count - 1)],
                //     position, Quaternion.identity);
                break;
        }
        // Vector3 position = new Vector3(0, 0, _obstacle.transform.position.z + _lengthPlatformObstacles);
        // _obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Count - 1)],
        //     position, Quaternion.identity);
        // Vector3 position = new Vector3(0, 0, _obstacle.transform.position.z + _lengthPlatformObstacles);
        // _obstacle = Instantiate(_obstaclesJungle[Random.Range(0, _obstaclesJungle.Count - 1)],
        //     position, Quaternion.identity);
    }

    private void SpawnObstacle(List<GameObject> obstacles)
    {
        Vector3 position = Vector3.zero;

        if (_currentObstacle != null)
        {
            position = new Vector3(
                0,
                0,
                _currentObstacle.transform.position.z + _lengthPlatformObstacles
            );
        }
        else
        {
            position = transform.position;
        }

        _currentObstacle = Instantiate(
            obstacles[Random.Range(0, obstacles.Count)],
            position,
            Quaternion.identity
        );
    }
}
