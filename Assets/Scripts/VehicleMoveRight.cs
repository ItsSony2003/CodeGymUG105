using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMoveRight : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
