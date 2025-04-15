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
    private bool _verticalRecoilFlag = false;
    private bool _horizonRecoilFlag = false;
    Quaternion cameraQuate= Quaternion.identity;
    // ���R�C����
    private float _resetNum = 0.005f;
    // �����Ŏ��_���オ�肫��܂ł̎���
    private float _recoilTime = 0.1f;
    [SerializeField]
    GameObject _player = null;
    private Quaternion _playerQuate = Quaternion.identity;

    [SerializeField]
    GameObject _object;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        Recoil();

    }

    private void Shot()
    {
        if (!PlayerManager.PlayerBulletMagazinCheck())
            return;
        PlayerManager.PlayerBulletShot();
        Vector3 lay = BulletManager.RayHitTest(transform.position, transform.forward);
        LayHitTest(lay);
        cameraQuate = transform.localRotation;
        _playerQuate = _player.transform.localRotation;
        // �c��������
        cameraQuate.x -= _verticalRecoil;
        _verticalRecoilFlag=true;
        // 0�Ȃ�c�ɂ܂������A1��2�ŉ��ɂԂ��
        int random = Random.Range(0, 2);
        if (random == 0) return;
        if (random == 1)
            // ����������
            _playerQuate.y -= _horizonRecoil; 
        else
            // ����������
            _playerQuate.y += _horizonRecoil;

    }

    private void LayHitTest(Vector3 lay)
    {
        GameObject gameobject = Instantiate(_object);
        gameobject.transform.position = lay;
    }

    // ����
    private void Recoil()
    {
        if (!_verticalRecoilFlag) return;
        // ��������
        transform.localRotation = Quaternion.Slerp(transform.localRotation, cameraQuate, _recoilTime);
        // ����������
        _player.transform.localRotation = Quaternion.Slerp(_player.transform.localRotation
            , _playerQuate
            , _recoilTime);
        //// ��R�C������������Ƃ�Slerp�𑱂����Ȃ�
        //float numX = transform.localRotation.x - cameraQuate.x;
        //numX-=LookCamera.recoilNum/2;
        //if ((numX <= _resetNum)) _verticalRecoilFlag = false;

        // �}�E�X�̓��������m������Slerp��ʂ�Ȃ�����
        float mouseCameraX = Input.GetAxis("Mouse X");
        float mauseCameraY = Input.GetAxis("Mouse Y");
        if( mouseCameraX >= 0.1f || mauseCameraY >= 0.1f )
            _verticalRecoilFlag = false;

    }


}
