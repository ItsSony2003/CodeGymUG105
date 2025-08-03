using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinMachine : MonoBehaviour
{
    public static SpawnCoinMachine instance;

    ObjectPool coinPool;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        coinPool = gameObject.GetComponent<ObjectPool>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnCoinOnGround(GameObject ground, Transform center)
    {
        int numberSpawn = Random.Range(1, 4);

        Vector3 groundCenter = center.transform.position;

        int groundSizeWidth = 19;
        int groundSizeDepth = 11;

        for(int i = 0; i <= numberSpawn;)
        {
            int randomX = Random.Range(-groundSizeWidth / 2, groundSizeWidth / 2);
            int randomZ = Random.Range(-groundSizeDepth / 2, groundSizeDepth / 2);

            Vector3 newPos = new Vector3(groundCenter.x + randomX, 1 , groundCenter.z + randomZ);

            if (CanSpawnCoinInHere(newPos))
            {
                coinPool.GetObj().transform.position = newPos;
                i++;
            }

        }
    }

    public bool CanSpawnCoinInHere(Vector3 pos)
    {
        int obstacleLayer = LayerMask.GetMask("Obstacle");
        Vector3 extend = new Vector3(0.8f, 1 , 0.8f);

        bool hasObject = Physics.CheckBox(pos, extend , Quaternion.identity, obstacleLayer);

        if (hasObject)
        {
            Debug.Log("Có vật cản ở đây!");

            return false;
        }

        return true;
    }
}
