using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField]
    private float _deadzoneX = 0;
    [SerializeField]
    private float _deadzoneY = 0;
    [SerializeField]
    private GameObject _player;

    static public float recoilNum = 0.0f;

    private readonly float _LOOKDOWNLIMIT = 0.7f;
    private readonly float _LOOKUPLIMIT = -0.7f;

    private float _normalSensitivity = 0.5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // ポーズを開いているときはマウスで視点移動させない
        if (UIManager.Instance.IsPause())
        {
            SettingSensitivity();
            return;
        }
        MouseCameraRotation();
    }

    private void MouseCameraRotation()
    {
        //マウスカーソルで左右視点+横回転
        float mouseCameraX = Input.GetAxis("Mouse X");
        recoilNum = mouseCameraX;
        mouseCameraX = mouseCameraX * _normalSensitivity;
        if (Mathf.Abs(mouseCameraX) >= _deadzoneX)
        {
            _player.transform.RotateAround(_player.transform.position, Vector3.up, mouseCameraX);

        }
        float mouseCameraY = Input.GetAxis("Mouse Y");
        mouseCameraY = mouseCameraY * _normalSensitivity;
        if (Mathf.Abs(mouseCameraY) >= _deadzoneY)
        {
            transform.RotateAround(transform.position, Vector3.right, -mouseCameraY);
        }
        LookLimit();
        Quaternion quate = transform.rotation;
        quate.y = 0f;
        quate.z = 0f;
        transform.localRotation = quate;
    }

    private void LookLimit()
    {
        if (transform.localRotation.x > _LOOKDOWNLIMIT)
        {
            Quaternion quate = transform.localRotation;
            quate.x = _LOOKDOWNLIMIT;
            transform.localRotation = quate;
        }
        if (transform.localRotation.x < _LOOKUPLIMIT)
        {
            Quaternion quate = transform.localRotation;
            quate.x = _LOOKUPLIMIT;
            transform.localRotation = quate;
        }
    }

    private void SettingSensitivity()
    {
        _normalSensitivity = OptionManager.Instance.GetNormalSensivity();
    }

}
