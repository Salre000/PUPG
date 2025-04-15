using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    // 縦反動値
    private float _verticalRecoil = 0.01f;
    // 横反動値
    private float _horizonRecoil = 0.01f;
    // リコイル開始/終了
    private bool _verticalRecoilFlag = false;
    private bool _horizonRecoilFlag = false;
    Quaternion cameraQuate= Quaternion.identity;
    // リコイルで
    private float _resetNum = 0.005f;
    // 反動で視点が上がりきるまでの時間
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
        // 縦反動実装
        cameraQuate.x -= _verticalRecoil;
        _verticalRecoilFlag=true;
        // 0なら縦にまっすぐ、1と2で横にぶれる
        int random = Random.Range(0, 2);
        if (random == 0) return;
        if (random == 1)
            // 横反動実装
            _playerQuate.y -= _horizonRecoil; 
        else
            // 横反動実装
            _playerQuate.y += _horizonRecoil;

    }

    private void LayHitTest(Vector3 lay)
    {
        GameObject gameobject = Instantiate(_object);
        gameobject.transform.position = lay;
    }

    // 反動
    private void Recoil()
    {
        if (!_verticalRecoilFlag) return;
        // 反動実装
        transform.localRotation = Quaternion.Slerp(transform.localRotation, cameraQuate, _recoilTime);
        // 横反動実装
        _player.transform.localRotation = Quaternion.Slerp(_player.transform.localRotation
            , _playerQuate
            , _recoilTime);
        //// れコイル制御をしたときSlerpを続かせない
        //float numX = transform.localRotation.x - cameraQuate.x;
        //numX-=LookCamera.recoilNum/2;
        //if ((numX <= _resetNum)) _verticalRecoilFlag = false;

        // マウスの動きを検知したらSlerpを通らなくする
        float mouseCameraX = Input.GetAxis("Mouse X");
        float mauseCameraY = Input.GetAxis("Mouse Y");
        if( mouseCameraX >= 0.1f || mauseCameraY >= 0.1f )
            _verticalRecoilFlag = false;

    }


}
