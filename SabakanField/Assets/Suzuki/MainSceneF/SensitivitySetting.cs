using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetting : UIBase
{
    // �ύX���ꂽ���x��ݒ�

    private Slider _normalSensi;
    private TMP_InputField _normalSensiInput;
    private StringBuilder _stringBuilder = new StringBuilder();

    private float _normalSensiValue = 0.5f;
    // ���x���
    private const float _NORMAL_SENSI_MAX = 1.0f;

    public override void Execute()
    {
        // �X���C�_�[�̕ύX
        if (_normalSensiValue != _normalSensi.value)
            NormalSensiSliderSetting();
        // �e�L�X�g����̕ύX
        if (_stringBuilder.ToString() != _normalSensiInput.text)
            NormalSensiInputFieldSetting();

    }

    public override void Initialize()
    {
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);
    }

    // �X���C�_�[�ł̕ύX
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        _stringBuilder.Append(_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // ���̓t�B�[���h�ł̕ύX
    private void NormalSensiInputFieldSetting()
    {
        _stringBuilder.Clear();
        if (!float.TryParse(_normalSensiInput.text,out _normalSensiValue))
        {
            // �����ȊO�̓��́A�G���[�v��
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
