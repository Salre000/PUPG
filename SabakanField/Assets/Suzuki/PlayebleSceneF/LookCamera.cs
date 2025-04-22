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
    // ���_�ړ�
    float mouseCameraX = 0.0f;
    float mouseCameraY = 0.0f;
    // ���E�l�̐ݒ�l
    private readonly float _LOOKDOWNLIMIT = 0.7f;
    private readonly float _LOOKUPLIMIT = -0.7f;
    // ���x
    private float _normalSensitivity = 0.5f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // �|�[�Y���J���Ă���Ƃ��̓}�E�X�Ŏ��_�ړ������Ȃ�
        if (UIManager.Instance.IsPause())
        {
            SettingSensitivity();
            return;
        }
        MouseCameraRotation();
    }

    private void MouseCameraRotation()
    {
        //�}�E�X�J�[�\���ō��E���_+����]
        mouseCameraX = Input.GetAxis("Mouse X");
        float value = mouseCameraX * _normalSensitivity;
        recoilNum = value;
        if (Mathf.Abs(mouseCameraX) >= _deadzoneX)
        {
            _player.transform.RotateAround(_player.transform.position, Vector3.up, mouseCameraX);

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

    // �㉺�������̌��E�l
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

    // ���x�ݒ�̕R�Â�
    private void SettingSensitivity()
    {
        _normalSensitivity = OptionManager.Instance.GetNormalSensitivity();
    }

}
