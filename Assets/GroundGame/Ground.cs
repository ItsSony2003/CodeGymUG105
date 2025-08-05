using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Transform spawnPos;
    public Transform center;
    bool hasCrossed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCrossed) return;

        if (other.GetComponent<Player>())
        {
            GroundManager.Instance.newGround();
            hasCrossed = true;

        }
    }
}
