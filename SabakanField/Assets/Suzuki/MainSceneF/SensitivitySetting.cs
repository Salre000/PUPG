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
    }

    public override void Initialize()
    {
        _stringBuilder.Clear();
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);

        // 入力完了時に発火するよう設定
        _normalSensiInput.onEndEdit.AddListener(addListener=>NormalSensiInputFieldSetting());
    }

    // スライダーでの変更
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        _stringBuilder.AppendFormat("{0:F2}",_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // 入力フィールドでの変更
    private void NormalSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_normalSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _normalSensiValue = tryValue;

        // 感度上限オーバー対策
        if (_normalSensiValue >= _NORMAL_SENSI_MAX)
            _normalSensiValue = _NORMAL_SENSI_MAX;

        // 感度を反映
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}",_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _normalSensi.value = _normalSensiValue;
    }
}
