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
        // Player�̑O�㍶�E�̈ړ�
        float xMovement = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime; // ���E�̈ړ�
        float zMovement = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime; // �O��̈ړ�
        Vector3 playerPosition = transform.position;
        transform.Translate(xMovement,0, zMovement);

    }
}
