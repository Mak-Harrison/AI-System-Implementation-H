using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The player
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Camera height & distance
    public float smoothSpeed = 5f;  // Smoothing speed

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Always face the player
    }
}
