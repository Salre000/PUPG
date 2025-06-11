using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWepon : MonoBehaviour
{
    // 武器を拾って入れ替える

    // プレイヤーインベントリー
    [SerializeField] private GameObject _inventory = null;
    // プレイヤーカメラ
    [SerializeField] private GameObject _playerCamera = null;
    // 今持ってるもの
    GameObject _equipmentWepon = null;
    // 拾うもの
    [SerializeField] GameObject _picWepon = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_picWepon!=null)
        {
            _equipmentWepon = _inventory.transform.GetChild(0).gameObject;
            Instantiate(_picWepon, _inventory.transform);
            Destroy(_equipmentWepon, 0.5f);
            _playerCamera.SetActive(true);
            PlayerManager.SetIsPicWepon(true);
            _picWepon = null;
        }
    }

    public void SetPicWepan(GameObject picObject) { _picWepon = picObject; }
}
