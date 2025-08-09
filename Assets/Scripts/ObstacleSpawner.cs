using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleSpawnPoint[] spawnPointsLeft;
    public ObstacleSpawnPoint[] spawnPointsRight;

    public ObjectPoolObtac poolLeft;
    public ObjectPoolObtac poolRight;

    public float spawnInterval = 0.5f;
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
            SpawnObstacles(spawnPointsLeft, poolLeft);
            SpawnObstacles(spawnPointsRight, poolRight);
            timer = spawnInterval;
        }
    }

    //void SpawnObstacles(ObstacleSpawnPoint[] spawnPoints, ObjectPool pool)
    //{
    //    foreach (var spawn in spawnPoints)
    //    {
    //        if (spawn.spawnPoint == null)
    //        {
    //            Debug.LogWarning($"⚠️ SpawnPoint '{spawn.name}' chưa gán Transform.");
    //            continue;
    //        }

    //        GameObject prefabToSpawn = null;

    //        if (spawn.isFixed)
    //        {
    //            prefabToSpawn = spawn.fixedPrefab;
    //            if (prefabToSpawn == null)
    //            {
    //                Debug.LogWarning($"⚠️ '{spawn.name}' là điểm cố định nhưng chưa gán prefab.");
    //                continue;
    //            }
    //        }
    //        else
    //        {
    //            if (spawn.obstaclePrefabs == null || spawn.obstaclePrefabs.Length == 0)
    //            {
    //                Debug.LogWarning($"⚠️ '{spawn.name}' chưa có danh sách prefab để random.");
    //                continue;
    //            }

    //            int rand = Random.Range(0, spawn.obstaclePrefabs.Length);
    //            prefabToSpawn = spawn.obstaclePrefabs[rand];
    //        }

    //        // Lấy từ Object Pool tương ứng
    //        GameObject pooledObj = pool.GetObjFromPrefab(prefabToSpawn);
    //        if (pooledObj != null)
    //        {
    //            pooledObj.transform.position = spawn.spawnPoint.position;
    //            pooledObj.transform.rotation = prefabToSpawn.transform.rotation;
    //            pooledObj.SetActive(true);
    //        }
    //    }
    //}
    void SpawnObstacles(ObstacleSpawnPoint[] spawnPoints, ObjectPoolObtac pool)
    {
        foreach (var spawn in spawnPoints)
        {
            if (spawn.spawnPoint == null)
            {
                continue;
            }

            GameObject prefabToSpawn = null;

            if (spawn.isFixed)
            {
                prefabToSpawn = spawn.fixedPrefab;
                if (prefabToSpawn == null)
                {
                    continue;
                }
            }
            else
            {
                GameObject[] candidatePrefabs = spawn.obstaclePrefabs;

                // Nếu chưa gán danh sách prefab, tự lấy từ ObjectPool
                if (candidatePrefabs != null && candidatePrefabs.Length == 0)
                {
                    candidatePrefabs = pool.GetPrefabsArray();
                    if (candidatePrefabs == null || candidatePrefabs.Length == 0)
                    {
                        continue;
                    }
                }

                int rand = Random.Range(0, candidatePrefabs.Length);
                prefabToSpawn = candidatePrefabs[rand];
            }

            //Lấy từ Object Pool tương ứng
            GameObject pooledObj = pool.GetObjFromPrefab(prefabToSpawn);
            if (pooledObj != null)
            {
                pooledObj.transform.rotation = spawn.spawnPoint.rotation;

                pooledObj.transform.position = spawn.spawnPoint.position;

                pooledObj.SetActive(true);
            }
        }
    }

}
