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
    private float _normalSensitivity = 0.5f;
    private float _adsSensitivity = 0.5f;

    // ���x�ݒ�
    [SerializeField] private int _masterValue = 0;
    private int _bgmValue = 0;
    private int _seValue = 0;


    AdsTypeSetting _adsTypeSetting = new AdsTypeSetting();
    SensitivitySetting _sensitivitySetting = new SensitivitySetting();
    VolumeSetting _volueSetting = new VolumeSetting();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        Initialize();
    }

    private void Initialize()
    {
        StartOption start= new StartOption();
        SetNormalSensitivity(OptionDataClass.GetNormalSensitivity());
        SetAdsSensitivity(OptionDataClass.GetAdsSensitivity());
        SetMasterVolume(OptionDataClass.GetMasterVolume());
        SetBGMVolume(OptionDataClass.GetBGMVolume());
        SetSEVolume(OptionDataClass.GetSEVolume());
        _adsType = OptionDataClass.GetAdsType();
        _adsTypeSetting.Initialize();
        _sensitivitySetting.Initialize();
        _volueSetting.Initialize();
    }

    private void Update()
    {
        if (!UIManager.Instance.IsPause()) return;
        _sensitivitySetting.Execute();
        _volueSetting.Execute();
    }

    // ADS���@
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
    // ���x�ݒ�
    public void SetNormalSensitivity(float value) { _normalSensitivity = value; }
    public float GetNormalSensitivity() { return _normalSensitivity; }
    public void SetAdsSensitivity(float value) { _adsSensitivity = value; }
    public float GetAdsSensitivity() { return _adsSensitivity; }
    //���ʐݒ�
    public void SetMasterVolume(int value) { _masterValue = value; }
    public int GetMasterVolume() { return _masterValue; }

    public void SetBGMVolume(int value) { _bgmValue = value; }
    public int GetBGMVolume() { return _bgmValue; }

    public void SetSEVolume(int value) { _seValue = value; }
    public int GetSEVolume() { return _seValue; }

    // �I�v�V�����ݒ�̃f�[�^�ۑ�
    public void SaveOptionData()
    {
        OptionDataClass.OptionDataSave(
            GetNormalSensitivity(),
            GetAdsSensitivity(),
            GetAdsType(),
            GetMasterVolume(),
            GetBGMVolume(),
            GetSEVolume()
            );
    }

}
