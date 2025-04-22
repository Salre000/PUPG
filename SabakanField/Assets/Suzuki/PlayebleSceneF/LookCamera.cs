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
    // 視点移動
    float mouseCameraX = 0.0f;
    float mouseCameraY = 0.0f;
    // 限界値の設定値
    private readonly float _LOOKDOWNLIMIT = 0.7f;
    private readonly float _LOOKUPLIMIT = -0.7f;
    // 感度
    private float _normalSensitivity = 0.5f;
    private float _adsSensitivity = 0.5f;

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
        if (!PlayerManager.IsAds())
            MouseCameraRotation();
        else
            AdsMouseCameraRotation();


    }

    // 通常視点
    private void MouseCameraRotation()
    {
        //マウスカーソルで左右視点+横回転
        mouseCameraX = Input.GetAxis("Mouse X");
        float value = mouseCameraX * _normalSensitivity;
        recoilNum = value;
        if (Mathf.Abs(mouseCameraX) >= _deadzoneX)
        {
            _player.transform.RotateAround(_player.transform.position, Vector3.up, value);

        }
        mouseCameraY = Input.GetAxis("Mouse Y");
        value = mouseCameraY * _normalSensitivity;
        if (Mathf.Abs(mouseCameraY) >= _deadzoneY)
        {
            transform.RotateAround(transform.position, Vector3.right, -value);
        }
        LookLimit();
        Quaternion quate = transform.rotation;
        quate.y = 0f;
        quate.z = 0f;
        transform.localRotation = quate;
    }

    // ADS視点
    private void AdsMouseCameraRotation()
    {
        //マウスカーソルで左右視点+横回転
        mouseCameraX = Input.GetAxis("Mouse X");
        float value = mouseCameraX * _adsSensitivity;
        recoilNum = value;
        if (Mathf.Abs(mouseCameraX) >= _deadzoneX)
        {
            _player.transform.RotateAround(_player.transform.position, Vector3.up, value);

        }
        mouseCameraY = Input.GetAxis("Mouse Y");
        value = mouseCameraY * _adsSensitivity;
        if (Mathf.Abs(mouseCameraY) >= _deadzoneY)
        {
            transform.RotateAround(transform.position, Vector3.right, -value);
        }
        LookLimit();
        Quaternion quate = transform.rotation;
        quate.y = 0f;
        quate.z = 0f;
        transform.localRotation = quate;
    }


    // 上下向く時の限界値
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

    // 感度設定の紐づけ
    private void SettingSensitivity()
    {
        _normalSensitivity = OptionManager.Instance.GetNormalSensitivity();
        _adsSensitivity = OptionManager.Instance.GetAdsSensitivity();
    }

}
