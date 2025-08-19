using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundPlayer : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate around Y axis (spin horizontally)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
