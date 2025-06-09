using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWepon : MonoBehaviour
{
    // ������E���ē���ւ���

    // �v���C���[�C���x���g���[
    [SerializeField] private GameObject _inventory = null;
    // �������Ă����
    GameObject _equipmentWepon=null;
    // �E������
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
