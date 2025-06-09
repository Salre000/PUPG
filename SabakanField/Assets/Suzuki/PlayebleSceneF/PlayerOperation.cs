using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOperation : MonoBehaviour
{
    [SerializeField, Header("歩き状態の速度")]
    private float _walkSpeed = 4.0f;
    [SerializeField, Header("走り状態の速度")]
    private float _dashSpeed = 7.5f;
    // 現在の1秒あたりの移動速度
    private float _speed = 0.0f;

    // 現在持っている銃
    [SerializeField, Header("現在持っている銃")]
    private GameObject _rifel;
    // 銃のposition管理
    private Vector3 _rifelPosition;
    // MainCamera
    private Camera _camera;
    // カメラのField of View制御の値
    private float _fieldOfView;
    // どれくらいズームするか
    private const float _FIELD_OF_VIEW = 30.0f;
    // リセット値
    // 銃のADSしてないときの位置
    private float _resetRifelX;
    // 切り替え式ADSの時用フラグ
    private bool _isAdsType = false;
    // ADS中か判定フラグ
    private bool _isAds = false;
    // メインカメラのADSしてないときの位置
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
        // デバック自滅
        if(Input.GetKeyUp(KeyCode.K)) PlayerManager.SetIsPlayerDead(true);

        // ポーズを開いているとき移動できないようにする
        if (UIManager.Instance.IsPause()) return;
        // 死んでいるとき
        if (PlayerManager.GetIsPlayerDead()) return;
        Movement();
        Ads();
    }

    private void Movement()
    {
        Dash();
        // Playerの前後左右の移動
        float xMovement = Input.GetAxis("Horizontal") * _speed * Time.deltaTime; // 左右の移動
        float zMovement = Input.GetAxis("Vertical") * _speed * Time.deltaTime; // 前後の移動
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
        // 右クリックでADS
        // true:切り替え false:長押し
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
        // 銃を真正面に
        _rifelPosition.x = 0;
        _rifel.transform.localPosition = _rifelPosition;

        // カメラをズーム
        _fieldOfView = _FIELD_OF_VIEW;
        _camera.fieldOfView = _fieldOfView;
    }

    private void AdsReset()
    {
        _isAds = false;
        // 銃を元の位置に
        _rifelPosition.x = _resetRifelX;
        _rifel.transform.localPosition = _rifelPosition;

        // カメラズームを元の値に
        _fieldOfView = _resetView;
        _camera.fieldOfView = _fieldOfView;
    }

}
