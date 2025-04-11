using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 10.0f;

    void Update()
    {
        Movement();



    }

    private void Movement()
    {
        // Playerの前後左右の移動
        float xMovement = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime; // 左右の移動
        float zMovement = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime; // 前後の移動
        Vector3 playerPosition = transform.position;
        transform.Translate(xMovement,0, zMovement);

    }
}
