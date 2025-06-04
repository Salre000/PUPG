using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    // �c�����l
    private float _verticalRecoil = 0.01f;
    // �������l
    private float _horizonRecoil = 0.01f;
    // ���R�C���J�n/�I��
    private bool _recoilXFlag = false;
    private bool _recoilYFlag = false;
    private Quaternion _cameraQuate = Quaternion.identity;
    // �}�E�X�𑀍삵�ĂȂ��Ƃ������Ō��̎����ɖ߂�l
    //private float _resetNumX = 0.01f;
    //private float _resetNumY = 0.01f;
    // �����Ŏ��_���オ�肫��܂ł̎���
    private float _recoilTime = 0.05f;
    [SerializeField]
    private GameObject _player = null;
    private Quaternion _playerQuate = Quaternion.identity;

    // �f�o�b�O�Ń��C�����������Ƃ����m�F����p
    [SerializeField]
    GameObject _object;


    // Update is called once per frame
    void Update()
    {
        // �|�[�Y��ʂ��L���ɂȂ��Ă���Ƃ������Ȃ�
        // ���R�C�����|�[�Y�A�łŖ����ɂ���̂�h������Recoil�𒆂ł����s�����Ƃ�
        if (UIManager.Instance.IsPause())
        {
            Recoil();
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        Recoil();

    }

    private void Shot()
    {
        if (!BulletManager.PlayerBulletMagazinCheck())
            return;
        BulletManager.PlayerBulletShot();
        Vector3 lay = BulletMoveFunction.AliceRayHitTest(transform.position, transform.forward);
        LayHitTest(lay);
        _cameraQuate = transform.localRotation;
        _playerQuate = _player.transform.localRotation;
        // �c��������
        _cameraQuate.x -= _verticalRecoil;
        _recoilXFlag = true;
        // 0�Ȃ�c�ɂ܂������A1��2�ŉ��ɂԂ��
        int random = Random.Range(0, 2 + 1);
        if (random == 0) return;
        if (random == 1)
            // ����������
            _playerQuate.y -= _horizonRecoil;
        else
            // ����������
            _playerQuate.y += _horizonRecoil;
        _recoilYFlag = true;
    }

    private void LayHitTest(Vector3 lay)
    {
        GameObject gameobject = Instantiate(_object);
        gameobject.transform.position = lay;
    }

    // ����
    private void Recoil()
    {
        if (!_recoilXFlag && !_recoilYFlag) return;
        if (_recoilXFlag)
            // ��������
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _cameraQuate, _recoilTime);
        if (_recoilYFlag)
            // ����������
            _player.transform.localRotation = Quaternion.Slerp(_player.transform.localRotation
            , _playerQuate
            , _recoilTime);

        // �}�E�X�̓��������m������Slerp��ʂ�Ȃ�����
        float mouseCameraX = Input.GetAxis("Mouse X");
        float mouseCameraY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseCameraX) >= _horizonRecoil)
            _recoilXFlag = false;
        if (Mathf.Abs(mouseCameraY) >= _verticalRecoil)
            _recoilYFlag = false;

    }


}
