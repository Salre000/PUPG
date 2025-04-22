using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetting : UIBase
{
    // 変更された感度を設定

    // 通常時の感度
    private Slider _normalSensi;
    private TMP_InputField _normalSensiInput;
    private float _normalSensiValue = 0.5f;
    // ADS時の感度
    private Slider _adsSensi;
    private TMP_InputField _adsSensiInput;
    private float _adsSensiValue = 0.5f;
    private StringBuilder _stringBuilder = new StringBuilder();

    // 感度上限
    private const float _SENSI_MAX = 1.0f;

    public override void Execute()
    {
        // スライダーの変更
        if (_normalSensiValue != _normalSensi.value)
            NormalSensiSliderSetting();
        if (_adsSensiValue != _adsSensi.value)
            AdsSensiSliderSetting();
    }

    public override void Initialize()
    {
        // 通常感度
        _stringBuilder.Clear();
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);
        // das感度
        _stringBuilder.Clear();
        _adsSensi = GameObject.Find("AdsSensitivitySlider").GetComponent<Slider>();
        _adsSensiInput = GameObject.Find("AdsSensitivityField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_adsSensiValue);

        // 入力完了時に発火するよう設定
        _normalSensiInput.onEndEdit.AddListener(addListener => NormalSensiInputFieldSetting());
        _adsSensiInput.onEndEdit.AddListener(addListener => AdsSensiInputFieldSetting());
    }

    // 通常 スライダーでの変更
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensitivity(_normalSensiValue);
        _stringBuilder.AppendFormat("{0:F2}", _normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // 通常 入力フィールドでの変更
    private void NormalSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_normalSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _normalSensiValue = tryValue;

        // 感度上限オーバー対策
        if (_normalSensiValue >= _SENSI_MAX)
            _normalSensiValue = _SENSI_MAX;

        // 感度を反映
        OptionManager.Instance.SetNormalSensitivity(_normalSensiValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}", _normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _normalSensi.value = _normalSensiValue;
    }

    // ADS スライダーでの変更
    private void AdsSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _adsSensiValue = _adsSensi.value;
        OptionManager.Instance.SetAdsSensitivity(_adsSensiValue);
        _stringBuilder.AppendFormat("{0:F2}", _adsSensiValue);
        _adsSensiInput.text = _stringBuilder.ToString();
    }

    // ADS 入力フィールドでの変更
    private void AdsSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_adsSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _adsSensiValue = tryValue;

        // 感度上限オーバー対策
        if (_adsSensiValue >= _SENSI_MAX)
            _adsSensiValue = _SENSI_MAX;

        // 感度を反映
        OptionManager.Instance.SetAdsSensitivity(_adsSensiValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}", _adsSensiValue);
        _adsSensiInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _adsSensi.value = _adsSensiValue;
    }
}
