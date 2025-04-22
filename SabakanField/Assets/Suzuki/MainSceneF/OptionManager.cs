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
    // ���x�ݒ�
    private float _normalSensivity=0.5f;

    AdsTypeSetting _adsTypeSetting=new AdsTypeSetting();
    SensitivitySetting _sensitivitySetting=new SensitivitySetting();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        Initialize();
    }

    private void Initialize()
    {
        _adsTypeSetting.Initialize();
        _sensitivitySetting.Initialize();
    }

    private void Update()
    {
        if(!UIManager.Instance.IsPause()) return;

        _sensitivitySetting.Execute();
    }

    // ADS���@
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
    // ���x�ݒ�
    public void SetNormalSensivity(float value) { _normalSensivity = value; }
    public float GetNormalSensivity() {  return _normalSensivity; }
}
