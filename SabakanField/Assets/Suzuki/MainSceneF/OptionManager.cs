using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    // オプション設定シングルトン
    public static OptionManager Instance;
    // adsする場合切り替えか長押しか true:切り替え false:長押し
    private bool _adsSetting = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // adsする場合切り替えか長押しか設定
    private void AdsSwitchSetting()
    {
        if(_adsSetting) _adsSetting=false;
        else _adsSetting=true;
    }


    public bool GetAdsSetting() { return _adsSetting; }
}
