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
    private bool _recoilXFlag = false;
    private bool _recoilYFlag = false;
    private Quaternion _cameraQuate = Quaternion.identity;
    // マウスを操作してないとき自動で元の視線に戻る値
    //private float _resetNumX = 0.01f;
    //private float _resetNumY = 0.01f;
    // 反動で視点が上がりきるまでの時間
    private float _recoilTime = 0.05f;
    [SerializeField]
    private GameObject _player = null;
    private Quaternion _playerQuate = Quaternion.identity;

    // デバッグでレイが当たったとこを確認する用
    [SerializeField]
    GameObject _object;


    // Update is called once per frame
    void Update()
    {
        // ポーズ画面が有効になっているとき撃たない
        // リコイルをポーズ連打で無しにするのを防ぐためRecoilを中でも実行させとく
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
        // 縦反動実装
        _cameraQuate.x -= _verticalRecoil;
        _recoilXFlag = true;
        // 0なら縦にまっすぐ、1と2で横にぶれる
        int random = Random.Range(0, 2 + 1);
        if (random == 0) return;
        if (random == 1)
            // 横反動実装
            _playerQuate.y -= _horizonRecoil;
        else
            // 横反動実装
            _playerQuate.y += _horizonRecoil;
        _recoilYFlag = true;
    }

    private void LayHitTest(Vector3 lay)
    {
        GameObject gameobject = Instantiate(_object);
        gameobject.transform.position = lay;
    }

    // 反動
    private void Recoil()
    {
        if (!_recoilXFlag && !_recoilYFlag) return;
        if (_recoilXFlag)
            // 反動実装
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _cameraQuate, _recoilTime);
        if (_recoilYFlag)
            // 横反動実装
            _player.transform.localRotation = Quaternion.Slerp(_player.transform.localRotation
            , _playerQuate
            , _recoilTime);

        // マウスの動きを検知したらSlerpを通らなくする
        float mouseCameraX = Input.GetAxis("Mouse X");
        float mouseCameraY = Input.GetAxis("Mouse Y");
        if (Mathf.Abs(mouseCameraX) >= _horizonRecoil)
            _recoilXFlag = false;
        if (Mathf.Abs(mouseCameraY) >= _verticalRecoil)
            _recoilYFlag = false;

    }


}
