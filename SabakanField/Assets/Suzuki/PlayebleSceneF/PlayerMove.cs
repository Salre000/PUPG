using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("������Ԃ̑��x")]
    private float _walkSpeed = 4.0f;
    [SerializeField,Header("�����Ԃ̑��x")]
    private float _dashSpeed = 7.5f;
    // ���݂�1�b������̈ړ����x
    private float _speed = 0.0f;

    // ���ݎ����Ă���e
    private GameObject _rifel;
    // MainCamera
    private Camera _camera;
    // �J������Field of View����̒l
    private float _fieldOfView;

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

    private void Ads()
    {
        // �E�N���b�N��ADS
        // true:�؂�ւ� false:������
        if (OptionManager.Instance.GetAdsSetting())
        {
            if (Input.GetMouseButtonDown(1))
            {

            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {

            }
        }

    }

}
