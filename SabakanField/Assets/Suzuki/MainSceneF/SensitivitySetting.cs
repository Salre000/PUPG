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
    }

    public override void Initialize()
    {
        _stringBuilder.Clear();
        _normalSensi = GameObject.Find("SensitivitySlider").GetComponent<Slider>();
        _normalSensiInput = GameObject.Find("SensitivityInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_normalSensiValue);

        // ���͊������ɔ��΂���悤�ݒ�
        _normalSensiInput.onEndEdit.AddListener(addListener=>NormalSensiInputFieldSetting());
    }

    // �X���C�_�[�ł̕ύX
    private void NormalSensiSliderSetting()
    {
        _stringBuilder.Clear();
        _normalSensiValue = _normalSensi.value;
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        _stringBuilder.AppendFormat("{0:F2}",_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
    }

    // ���̓t�B�[���h�ł̕ύX
    private void NormalSensiInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_normalSensiInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _normalSensiValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_normalSensiValue >= _NORMAL_SENSI_MAX)
            _normalSensiValue = _NORMAL_SENSI_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetNormalSensivity(_normalSensiValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}",_normalSensiValue);
        _normalSensiInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _normalSensi.value = _normalSensiValue;
    }
}
