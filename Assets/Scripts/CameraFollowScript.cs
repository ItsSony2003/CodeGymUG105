using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.7f;

    public static float lastPlayerMoveTime = 0f;
    public float delayBeforeScroll = 1.5f;

    public float zScrollSpeed = 2f; // how fast camera auto-moves forward

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        // if game haven't start, don't do anything
        if (!Player.gameStarted) return;

        // Only scroll camera if delay has passed
        if (Time.time - lastPlayerMoveTime >= delayBeforeScroll)
        {
            cameraOffset.z += zScrollSpeed * Time.deltaTime;
        }

        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
