using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsTypeSetting : MonoBehaviour
{
    // ads方法の初期設定
    Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        // チェックが付け外しされたときに発火する関数を指定
        _toggle.onValueChanged.AddListener(addListener=> OptionManager.Instance.ChangeAdsType());
    }
}
