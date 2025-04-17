using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Header("歩き状態の速度")]
    private float _walkSpeed = 4.0f;
    [SerializeField,Header("走り状態の速度")]
    private float _dashSpeed = 7.5f;
    // 現在の1秒あたりの移動速度
    private float _speed = 0.0f;

    // 現在持っている銃
    private GameObject _rifel;
    // MainCamera
    private Camera _camera;
    // カメラのField of View制御の値
    private float _fieldOfView;

    void Update()
    {
        if(PlayerManager.IsPlayerDead()) return;
            Movement();
    }

    private void Movement()
    {
        Dash();
        // Playerの前後左右の移動
        float xMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime; // 左右の移動
        float zMovement = Input.GetAxis("Vertical") * _speed * Time.deltaTime; // 前後の移動
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
        // 右クリックでADS
        // true:切り替え false:長押し
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
