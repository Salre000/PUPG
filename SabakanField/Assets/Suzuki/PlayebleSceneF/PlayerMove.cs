using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 4.0f;
    [SerializeField]
    private float _dashSpeed = 7.5f;

    private float _speed = 0.0f;

    void Update()
    {
        if(PlayerManager.IsPlayerDead()) return;
            Movement();

    }

    private void Movement()
    {
        Dash();
        // Player�̑O�㍶�E�̈ړ�
        float xMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime; // ���E�̈ړ�
        float zMovement = Input.GetAxis("Vertical") * _speed * Time.deltaTime; // �O��̈ړ�
        Vector3 playerPosition = transform.position;
        transform.Translate(xMovement,0, zMovement);
    }

    private void Dash()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            _speed = _dashSpeed;
        else
            _speed=_walkSpeed;
    }


}
