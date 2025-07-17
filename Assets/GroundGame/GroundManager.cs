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
        if(groundHeihest == null)
        {
            groundHeihest = groundPool.GetObj();
            groundHeihest.transform.position = Vector3.zero;
        }
        else
        {
            Ground groundComp = groundHeihest.GetComponent<Ground>();
            groundHeihest = groundPool.GetObj();
            groundHeihest.transform.position = groundComp.spawnPos.transform.position;
       


        }
    }

    public void GetHighestGroundPos()
    {
        float highestZ = float.MinValue;

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
