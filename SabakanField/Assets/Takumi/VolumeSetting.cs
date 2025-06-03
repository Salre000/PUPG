using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : UIBase
{

    // 全体の音量設定
    private Slider _masterSlider;
    private TMP_InputField _masterSliderInput;
    private float _masterSliderValue = 2.5f;
    // BGMの音量設定
    private Slider _bgmSlider;
    private TMP_InputField _bgmSliderInput;
    private float _bgmSliderValue = 2.5f;
    // SEの音量設定
    private Slider _seSlider;
    private TMP_InputField _seSliderInput;
    private float _seSliderValue = 2.5f;



    private StringBuilder _stringBuilder = new StringBuilder();

    // 感度上限
    private const float _VOLUME_MAX = 100.0f;

    public override void Execute()
    {
        // スライダーの変更
        if (_masterSliderValue != _masterSlider.value)
            MasterSliderSliderSetting();
        if (_bgmSliderValue != _bgmSlider.value)
            BGMSliderSliderSetting();
        if (_seSliderValue != _seSlider.value)
            SESliderSliderSetting();
    }

    public override void Initialize()
    {
        // 全体音量
        _stringBuilder.Clear();
        _masterSlider = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
        _masterSliderInput = GameObject.Find("MasterVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_masterSliderValue);
        // BGM音量
        _stringBuilder.Clear();
        _bgmSlider = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>();
        _bgmSliderInput = GameObject.Find("BGMVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_bgmSliderValue);

        // SE音量
        _stringBuilder.Clear();
        _seSlider = GameObject.Find("SEVolumeSlider").GetComponent<Slider>();
        _seSliderInput = GameObject.Find("SEVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_seSliderValue);

        // 入力完了時に発火するよう設定
        _masterSliderInput.onEndEdit.AddListener(addListener => MasterInputFieldSetting());
        _bgmSliderInput.onEndEdit.AddListener(addListener => BGMInputFieldSetting());
        _seSliderInput.onEndEdit.AddListener(addListener => SEInputFieldSetting());

        // 見た目数値の再設定
        _masterSlider.value = OptionManager.Instance.GetMasterVolume();
        _bgmSlider.value = OptionManager.Instance.GetBGMVolume();
        _seSlider.value = OptionManager.Instance.GetSEVolume();

        MasterSliderSliderSetting();
        BGMSliderSliderSetting();
        SESliderSliderSetting();
    }

    // マスター スライダーでの変更
    private void MasterSliderSliderSetting()
    {
        _stringBuilder.Clear();
        _masterSliderValue = _masterSlider.value;
        OptionManager.Instance.SetMasterVolume((int)_masterSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _masterSliderValue);
        _masterSliderInput.text = _stringBuilder.ToString();
    }

    // マスター 入力フィールドでの変更
    private void MasterInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_masterSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _masterSliderValue = tryValue;

        // 感度上限オーバー対策
        if (_masterSliderValue >= _VOLUME_MAX)
            _masterSliderValue = _VOLUME_MAX;

        // 感度を反映
        OptionManager.Instance.SetMasterVolume((int)_masterSliderValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}", _masterSliderValue);
        _masterSliderInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _masterSlider.value = _masterSliderValue;
    }
    // マスター スライダーでの変更
    private void BGMSliderSliderSetting()
    {
        _stringBuilder.Clear();
        _bgmSliderValue = _bgmSlider.value;
        OptionManager.Instance.SetBGMVolume((int)_bgmSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _bgmSliderValue);
        _bgmSliderInput.text = _stringBuilder.ToString();
    }

    // マスター 入力フィールドでの変更
    private void BGMInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_bgmSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _bgmSliderValue = tryValue;

        // 感度上限オーバー対策
        if (_bgmSliderValue >= _VOLUME_MAX)
            _bgmSliderValue = _VOLUME_MAX;

        // 感度を反映
        OptionManager.Instance.SetBGMVolume((int)_bgmSliderValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}", _bgmSliderValue);
        _bgmSliderInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _bgmSlider.value = _bgmSliderValue;
    }
    // マスター スライダーでの変更
    private void SESliderSliderSetting()
    {
        _stringBuilder.Clear();
        _seSliderValue = _seSlider.value;
        OptionManager.Instance.SetSEVolume((int)_seSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _seSliderValue);
        _seSliderInput.text = _stringBuilder.ToString();
    }

    // マスター 入力フィールドでの変更
    private void SEInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_seSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _seSliderValue = tryValue;

        // 感度上限オーバー対策
        if (_seSliderValue >= _VOLUME_MAX)
            _seSliderValue = _VOLUME_MAX;

        // 感度を反映
        OptionManager.Instance.SetSEVolume((int)_seSliderValue);
        // テキストに小数第二位まで表示
        _stringBuilder.AppendFormat("{0:F2}", _seSliderValue);
        _seSliderInput.text = _stringBuilder.ToString();
        // スライダーに反映
        _seSlider.value = _seSliderValue;
    }

}
