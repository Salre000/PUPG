using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTypeSetting : UIBase
{
    // ads方法の初期設定
    Toggle _toggle;

    public override void Execute()
    {

    }

    public override void Initialize()
    {
        _toggle = GameObject.Find("AdsToggle").GetComponent<Toggle>();
        _toggle.isOn = OptionManager.Instance.GetAdsType();
        // チェックが付け外しされたときに発火する関数を指定
        _toggle.onValueChanged.AddListener(addListener => OptionManager.Instance.ChangeAdsType());
    }

}
