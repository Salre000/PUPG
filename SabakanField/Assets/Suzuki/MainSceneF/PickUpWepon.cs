using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWepon : MonoBehaviour
{
    // 武器を拾って入れ替える

    // プレイヤーインベントリー
    [SerializeField] private GameObject _inventory = null;
    // 今持ってるもの
    GameObject _equipmentWepon=null;
    // 拾うもの
    [SerializeField]GameObject _picWepon=null;

    // Start is called before the first frame update
    void Start()
    {
        _equipmentWepon=_inventory.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(_picWepon,_inventory.transform);
            Destroy(_equipmentWepon);
            PlayerManager.SetIsPicWepon(true);
        }
    }
}
