using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetting : UIBase
{
    // 変更された感度を設定

    private Slider _normalSensi;
    private TMP_InputField _normalSensiInput;
    private StringBuilder _stringBuilder = new StringBuilder();

    private float _normalSensiValue = 0.5f;
    // 感度上限
    private const float _NORMAL_SENSI_MAX = 1.0f;

    public override void Execute()
    {
        // スライダーの変更
        if (_normalSensiValue != _normalSensi.value)
            NormalSensiSliderSetting();
        // テキストからの変更
        if (_stringBuilder.ToString() != _normalSensiInput.text)
            NormalSensiInputFieldSetting();

    }

    public override void Initialize()
    {
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);
    }

    // スライダーでの変更
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        _stringBuilder.Append(_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // 入力フィールドでの変更
    private void NormalSensiInputFieldSetting()
    {
        _stringBuilder.Clear();
        if (!float.TryParse(_normalSensiInput.text,out _normalSensiValue))
        {
            // 数字以外の入力、エラー要因
            //_stringBuilder.Append(_normalSensiValue);
            //_normalSensiInput.text = _stringBuilder.ToString();
            //return;
        }

        if (_normalSensiValue >= _NORMAL_SENSI_MAX)
        {
            _normalSensiValue = _NORMAL_SENSI_MAX;
        }
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        _stringBuilder.Append(_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
        _normalSensi.value = _normalSensiValue;
    }
}
