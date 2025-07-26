using UnityEngine;

[System.Serializable]
public class ObstacleSpawnPoint
{
    public string name;
    public Transform spawnPoint;

    public bool isFixed; 
    public GameObject fixedPrefab; 

    public GameObject[] obstaclePrefabs; 
}
