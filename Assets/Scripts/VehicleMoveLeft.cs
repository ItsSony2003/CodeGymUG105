using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMoveLeft : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
