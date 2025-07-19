using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleSpawnPoint[] spawnPoints;   
    public float spawnInterval = 2f;           

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnObstacles();
            timer = spawnInterval;
        }
    }

    void SpawnObstacles()
    {
        foreach (var spawn in spawnPoints)
        {
            if (spawn.spawnPoint == null || spawn.obstaclePrefabs.Length == 0)
            {
                Debug.LogWarning($"⚠️ SpawnPoint '{spawn.name}' chưa gán prefab hoặc transform.");
                continue;
            }

            int randIndex = Random.Range(0, spawn.obstaclePrefabs.Length);
            GameObject prefab = spawn.obstaclePrefabs[randIndex];
            Instantiate(prefab, spawn.spawnPoint.position, prefab.transform.rotation);
        }
    }
}
