using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionManager : MonoBehaviour
{
    // オプション設定シングルトン
    public static OptionManager Instance;
    // ADSする場合切り替えか長押しか true:切り替え false:長押し
    private bool _adsType = false;
    // 感度設定
    private float _normalSensitivity=0.5f;
    private float _adsSensitivity = 0.5f;

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
        OptionDataClass.GetOptionData();
    }

    private void Update()
    {
        if(!UIManager.Instance.IsPause()) return;

        _sensitivitySetting.Execute();
    }

    // ADS方法
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
    // 感度設定
    public void SetNormalSensitivity(float value) { _normalSensitivity = value; }
    public float GetNormalSensitivity() {  return _normalSensitivity; }
    public void SetAdsSensitivity(float value) { _adsSensitivity = value; }
    public float GetAdsSensitivity() {  return _adsSensitivity; }
}
