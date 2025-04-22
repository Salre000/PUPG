using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySetting : UIBase
{
    // �ύX���ꂽ���x��ݒ�

    // �ʏ펞�̊��x
    private Slider _normalSensi;
    private TMP_InputField _normalSensiInput;
    private float _normalSensiValue = 0.5f;
    // ADS���̊��x
    private Slider _adsSensi;
    private TMP_InputField _adsSensiInput;
    private float _adsSensiValue = 0.5f;
    private StringBuilder _stringBuilder = new StringBuilder();

    // ���x���
    private const float _SENSI_MAX = 1.0f;

    public override void Execute()
    {
        // �X���C�_�[�̕ύX
        if (_normalSensiValue != _normalSensi.value)
            NormalSensiSliderSetting();
        if (_adsSensiValue != _adsSensi.value)
            AdsSensiSliderSetting();
    }

    public override void Initialize()
    {
        // �ʏ튴�x
        _stringBuilder.Clear();
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);
        // das���x
        _stringBuilder.Clear();
        _adsSensi = GameObject.Find("AdsSensitivitySlider").GetComponent<Slider>();
        _adsSensiInput = GameObject.Find("AdsSensitivityField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_adsSensiValue);

        // ���͊������ɔ��΂���悤�ݒ�
        _normalSensiInput.onEndEdit.AddListener(addListener => NormalSensiInputFieldSetting());
        _adsSensiInput.onEndEdit.AddListener(addListener => AdsSensiInputFieldSetting());
    }

    // �ʏ� �X���C�_�[�ł̕ύX
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensitivity(_normalSensiValue);
        _stringBuilder.AppendFormat("{0:F2}", _normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // �ʏ� ���̓t�B�[���h�ł̕ύX
    private void NormalSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_normalSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _normalSensiValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_normalSensiValue >= _SENSI_MAX)
            _normalSensiValue = _SENSI_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetNormalSensitivity(_normalSensiValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}", _normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _normalSensi.value = _normalSensiValue;
    }

    // ADS �X���C�_�[�ł̕ύX
    private void AdsSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _adsSensiValue = _adsSensi.value;
        OptionManager.Instance.SetAdsSensitivity(_adsSensiValue);
        _stringBuilder.AppendFormat("{0:F2}", _adsSensiValue);
        _adsSensiInput.text = _stringBuilder.ToString();
    }

    // ADS ���̓t�B�[���h�ł̕ύX
    private void AdsSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_adsSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _adsSensiValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_adsSensiValue >= _SENSI_MAX)
            _adsSensiValue = _SENSI_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetAdsSensitivity(_adsSensiValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}", _adsSensiValue);
        _adsSensiInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _adsSensi.value = _adsSensiValue;
    }
}
