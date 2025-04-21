using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionManager : MonoBehaviour
{
    // �I�v�V�����ݒ�V���O���g��
    public static OptionManager Instance;
    // ADS����ꍇ�؂�ւ����������� true:�؂�ւ� false:������
    private bool _adsType = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // ADS���@
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
}
