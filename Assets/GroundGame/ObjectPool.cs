using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*Dequeue : lay khua dau tien trong queue
//*Enqueue : dua vao cuoi queue

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> objPrefabs;
    public int poolSize = 10;
    private Queue<GameObject> poolQueue;

    private List<GameObject> usingObjList;


    private void Awake()
    {
        poolQueue = new Queue<GameObject>();
        usingObjList = new List<GameObject>();

        if (poolQueue == null && objPrefabs == null) return;

        this.poolSize = poolSize;

        for (int i = 0; i < this.poolSize; i++)
        {
            int randomIndex = Random.Range(0, objPrefabs.Count);
            GameObject obj = Instantiate(objPrefabs[randomIndex]);
            obj.transform.parent = transform;
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObj()
    {
        if (usingObjList.Count >= poolSize)
        {
            GameObject objToReturn = usingObjList[0];
            usingObjList.RemoveAt(0);
            ReturnObj(objToReturn);
        }

        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            usingObjList.Add(obj);
            return obj;
        }
        else
        {
            int randomIndex = Random.Range(0, objPrefabs.Count);
            GameObject obj = Instantiate(objPrefabs[randomIndex]);
            usingObjList.Add(obj);
            return obj;
        }
    }

    public void ReturnObj(GameObject obj)
    {
        obj.SetActive(false);
        usingObjList.Remove(obj);
        poolQueue.Enqueue(obj);
    }

    public Queue<GameObject> GetListQueue()
    {
        return poolQueue;
    }

    public List<GameObject> GetUsingList() 
    { 
        return usingObjList;
    }
}
