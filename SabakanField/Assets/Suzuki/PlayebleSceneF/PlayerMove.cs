using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float currentSpeed = 30.0f;

    void Update()
    {
        // Player�̑O�㍶�E�̈ړ�
        float xMovement = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime; // ���E�̈ړ�
        float zMovement = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime; // �O��̈ړ�
        transform.Translate(xMovement, 0, zMovement); // �I�u�W�F�N�g�̈ʒu���X�V



    }
}
