using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    // �A�[�}�[�������Ă������x�U����h���悤�ɂ���

    // �A�[�}�[�𑕒��������̉��A���߂��瑕�����Ă���ꍇ�͖炳�Ȃ�
    [SerializeField, Header("�A�[�}�[�𑕒��������̉�")] private AudioClip _getArmorSound;
    // �A�[�}�[�������Ȃ������̉�
    [SerializeField, Header("�A�[�}�[�������Ȃ������̉�")] private AudioClip _armorBreakSound;
    // �A�[�}�[�����ݑ������Ă��邩
    [SerializeField] private bool _isArmor = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.SetIsArmor(_isArmor);
    }

    // Update is called once per frame
    void Update()
    {
        // �f�o�b�O�p�A�[�}�[�t�^
        if (Input.GetKeyDown(KeyCode.M))
        {
            _isArmor = true;
        }
        if (PlayerManager.GetIsArmor() != _isArmor)
        {
            PlayerManager.SetIsArmor(_isArmor);
        }
    }

    // 
    private void Exesute()
    {

    }
}
