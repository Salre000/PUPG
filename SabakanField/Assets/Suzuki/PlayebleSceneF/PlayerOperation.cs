using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{
    [SerializeField, Header("������Ԃ̑��x")]
    private float _walkSpeed = 4.0f;
    [SerializeField, Header("�����Ԃ̑��x")]
    private float _dashSpeed = 7.5f;
    // ���݂�1�b������̈ړ����x
    private float _speed = 0.0f;

    // ���ݎ����Ă���e
    [SerializeField, Header("���ݎ����Ă���e")]
    private GameObject _rifel;
    // �e��position�Ǘ�
    private Vector3 _rifelPosition;
    // MainCamera
    private Camera _camera;
    // �J������Field of View����̒l
    private float _fieldOfView;
    // �ǂꂭ�炢�Y�[�����邩
    private const float _FIELD_OF_VIEW = 30.0f;
    // ���Z�b�g�l
    // �e��ADS���ĂȂ��Ƃ��̈ʒu
    private float _resetRifelX;
    // �؂�ւ���ADS�̎��p�t���O
    private bool _isAdsType = false;
    // ADS��������t���O
    private bool _isAds = false;
    // ���C���J������ADS���ĂȂ��Ƃ��̈ʒu
    private float _resetView;

    private void Awake()
    {
        _camera = GameObject.Find("MainCamera").GetComponent<Camera>();
        _fieldOfView = _camera.fieldOfView;
        _rifelPosition = _rifel.transform.localPosition;
        _resetRifelX = _rifel.transform.localPosition.x;
        _resetView = _camera.fieldOfView;
    }

    void Update()
    {
        // �f�o�b�N����
        if(Input.GetKeyUp(KeyCode.K)) PlayerManager.SetIsPlayerDead(true);

        // �|�[�Y���J���Ă���Ƃ��ړ��ł��Ȃ��悤�ɂ���
        if (UIManager.Instance.IsPause()) return;
        // ����ł���Ƃ�
        if (PlayerManager.GetIsPlayerDead()) return;
        Movement();
        Ads();
    }

    private void Movement()
    {
        Dash();
        // Player�̑O�㍶�E�̈ړ�
        float xMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime; // ���E�̈ړ�
        float zMovement = Input.GetAxis("Vertical") * _speed * Time.deltaTime; // �O��̈ړ�
        Vector3 playerPosition = transform.position;
        transform.Translate(xMovement, 0, zMovement);
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _speed = _dashSpeed;
        else
            _speed = _walkSpeed;
    }

    private void Ads()
    {
        // �E�N���b�N��ADS
        // true:�؂�ւ� false:������
        if (OptionManager.Instance.GetAdsType())
        {
            if (Input.GetMouseButtonDown(1))
            {
                if(_isAdsType) _isAdsType = false;
                else _isAdsType = true;
            }
            if (_isAdsType)
                AdsNow();
            else
                AdsReset();
        }
        else
        {
            if (Input.GetMouseButton(1))
                AdsNow();
            else
                AdsReset();
            _isAdsType = false;
        }
        PlayerManager.SetIsAds(_isAds);
    }

    private void AdsNow()
    {
        _isAds = true;
        // �e��^���ʂ�
        _rifelPosition.x = 0;
        _rifel.transform.localPosition = _rifelPosition;

        // �J�������Y�[��
        _fieldOfView = _FIELD_OF_VIEW;
        _camera.fieldOfView = _fieldOfView;
    }

    private void AdsReset()
    {
        _isAds = false;
        // �e�����̈ʒu��
        _rifelPosition.x = _resetRifelX;
        _rifel.transform.localPosition = _rifelPosition;

        // �J�����Y�[�������̒l��
        _fieldOfView = _resetView;
        _camera.fieldOfView = _fieldOfView;
    }

}
