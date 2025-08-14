using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.7f;

    public static float lastPlayerMoveTime = 0f;
    public static bool pauseAutoScroll = false;

    public float delayBeforeScroll = 1.5f;
    public float zScrollSpeed = 2f; // how fast camera auto-moves forward

    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        if (!Player.gameStarted) return;

        // Scroll only if: delay passed AND not paused by forward input
        if (!pauseAutoScroll && (Time.time - lastPlayerMoveTime >= delayBeforeScroll))
        {
            cameraOffset.z += zScrollSpeed * Time.deltaTime;
        }

        Vector3 newPosition = targetObject.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
    }
}
