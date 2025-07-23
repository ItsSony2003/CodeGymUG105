using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.7f;

    public float zScrollSpeed = 2f; // how fast camera auto-moves forward

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        // Step 1: Move camera forward on Z over time
        cameraOffset.z += zScrollSpeed * Time.deltaTime;

        // Step 2: Follow player’s position + offset (which is now drifting forward)
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
