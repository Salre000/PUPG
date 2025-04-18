using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    // �I�v�V�����ݒ�V���O���g��
    public static OptionManager Instance;
    // ads����ꍇ�؂�ւ����������� true:�؂�ւ� false:������
    private bool _adsSetting = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // ads����ꍇ�؂�ւ������������ݒ�
    private void AdsSwitchSetting()
    {
        if(_adsSetting) _adsSetting=false;
        else _adsSetting=true;
    }


    public bool GetAdsSetting() { return _adsSetting; }
}
