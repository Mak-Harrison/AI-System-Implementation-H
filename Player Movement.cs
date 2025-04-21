using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W)) moveZ = 1;
        if (Input.GetKey(KeyCode.S)) moveZ = -1;
        if (Input.GetKey(KeyCode.A)) moveX = -1;
        if (Input.GetKey(KeyCode.D)) moveX = 1;

        Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }
}
