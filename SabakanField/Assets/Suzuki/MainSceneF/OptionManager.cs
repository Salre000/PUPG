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

    // ADS方法
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
    // 感度設定
    public void SetNormalSensivity(float value) { _normalSensivity = value; }
    public float GetNormalSensivity() {  return _normalSensivity; }
}
