using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWepon : MonoBehaviour
{
    // ������E���ē���ւ���

    // �v���C���[�C���x���g���[
    [SerializeField] private GameObject _inventory = null;
    // �v���C���[�J����
    [SerializeField] private GameObject _playerCamera = null;
    // �������Ă����
    GameObject _equipmentWepon = null;
    // �E������
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
