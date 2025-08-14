using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*Dequeue : lay khua dau tien trong queue
//*Enqueue : dua vao cuoi queue

public class ObjectPool : MonoBehaviour
{
    public GameObject objPrefabs;
    public int poolSize = 10;
    private Queue<GameObject> poolQueue;
    public GameObject currentItem;

    public List<GameObject> usingObjList;


    private void Awake()
    {
        poolQueue = new Queue<GameObject>();
        usingObjList = new List<GameObject>();

        if (poolQueue == null && objPrefabs == null) return;
    }


    public void GeneratePool()
    {
        GameObject obj = Instantiate(objPrefabs);
        obj.transform.parent = transform;
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    public GameObject GetObj()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            usingObjList.Add(obj);
            currentItem = obj;
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objPrefabs);
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

    ////Lấy GameObject tương ứng
    public GameObject GetObjFromPrefab(GameObject targetPrefab)
    {
        foreach (var obj in usingObjList)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(targetPrefab.name))
            {
                usingObjList.Add(obj);
                return obj;
            }
        }

        foreach (var obj in poolQueue)
        {
            if (obj.name.Contains(targetPrefab.name))
            {
                poolQueue = new Queue<GameObject>(poolQueue);
                GameObject match = null;

                foreach (var q in poolQueue)
                {
                    if (q.name.Contains(targetPrefab.name))
                    {
                        match = q;
                        break;
                    }
                }

                if (match != null)
                {
                    poolQueue = new Queue<GameObject>(poolQueue);
                    poolQueue = new Queue<GameObject>(poolQueue);
                    poolQueue = new Queue<GameObject>(poolQueue);
                    poolQueue.Dequeue();
                    match.SetActive(true);
                    usingObjList.Add(match);
                    return match;
                }
            }
        }

        GameObject newObj = Instantiate(targetPrefab);
        newObj.SetActive(true);
        usingObjList.Add(newObj);
        return newObj;
    }

    public Queue<GameObject> GetListQueue()
    {
        return poolQueue;
    }

    public List<GameObject> GetUsingList() 
    { 
        return usingObjList;
    }

    public GameObject[] GetPrefabsArray()
    {
        return usingObjList.ToArray();
;
    }


}
