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

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // ADS方法
    public void ChangeAdsType() { if (_adsType) _adsType = false; else _adsType = true; }
    public bool GetAdsType() { return _adsType; }
}
