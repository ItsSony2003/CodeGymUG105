using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager Instance;

    public ObjectPool groundPool;

    private GameObject groundHeihest;


    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            newGround();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGround()
    {
        if(groundHeihest != null)
        {
            GetHighestGroundPos();
            Debug.Log(groundHeihest.name);
            Ground groundComp = groundHeihest.GetComponent<Ground>();
            groundPool.GetObj().transform.position = groundComp.spawnPos.transform.position;
        }
        else
        {
            groundPool.GetObj().transform.position = Vector3.zero;
        }
    }

    public void GetHighestGroundPos()
    {
        float highestZ = float.MinValue;

        //GameObject ground = null;

        foreach (GameObject ground in groundPool.GetListGround())
        {
            if (ground.activeInHierarchy && ground.transform.position.z > highestZ)
            {
                highestZ = ground.transform.position.z;

                groundHeihest = ground;
            }
        }

    }

}
