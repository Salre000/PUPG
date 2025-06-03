using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : UIBase
{

    // �S�̂̉��ʐݒ�
    private Slider _masterSlider;
    private TMP_InputField _masterSliderInput;
    private float _masterSliderValue = 2.5f;
    // BGM�̉��ʐݒ�
    private Slider _bgmSlider;
    private TMP_InputField _bgmSliderInput;
    private float _bgmSliderValue = 2.5f;
    // SE�̉��ʐݒ�
    private Slider _seSlider;
    private TMP_InputField _seSliderInput;
    private float _seSliderValue = 2.5f;



    private StringBuilder _stringBuilder = new StringBuilder();

    // ���x���
    private const float _VOLUME_MAX = 100.0f;

    public override void Execute()
    {
        // �X���C�_�[�̕ύX
        if (_masterSliderValue != _masterSlider.value)
            MasterSliderSliderSetting();
        if (_bgmSliderValue != _bgmSlider.value)
            BGMSliderSliderSetting();
        if (_seSliderValue != _seSlider.value)
            SESliderSliderSetting();
    }

    public override void Initialize()
    {
        // �S�̉���
        _stringBuilder.Clear();
        _masterSlider = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
        _masterSliderInput = GameObject.Find("MasterVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_masterSliderValue);
        // BGM����
        _stringBuilder.Clear();
        _bgmSlider = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>();
        _bgmSliderInput = GameObject.Find("BGMVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_bgmSliderValue);

        // SE����
        _stringBuilder.Clear();
        _seSlider = GameObject.Find("SEVolumeSlider").GetComponent<Slider>();
        _seSliderInput = GameObject.Find("SEVolumeInputField").GetComponent<TMP_InputField>();
        _stringBuilder.Append(_seSliderValue);

        // ���͊������ɔ��΂���悤�ݒ�
        _masterSliderInput.onEndEdit.AddListener(addListener => MasterInputFieldSetting());
        _bgmSliderInput.onEndEdit.AddListener(addListener => BGMInputFieldSetting());
        _seSliderInput.onEndEdit.AddListener(addListener => SEInputFieldSetting());

        // �����ڐ��l�̍Đݒ�
        _masterSlider.value = OptionManager.Instance.GetMasterVolume();
        _bgmSlider.value = OptionManager.Instance.GetBGMVolume();
        _seSlider.value = OptionManager.Instance.GetSEVolume();

        MasterSliderSliderSetting();
        BGMSliderSliderSetting();
        SESliderSliderSetting();
    }

    // �}�X�^�[ �X���C�_�[�ł̕ύX
    private void MasterSliderSliderSetting()
    {
        _stringBuilder.Clear();
        _masterSliderValue = _masterSlider.value;
        OptionManager.Instance.SetMasterVolume((int)_masterSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _masterSliderValue);
        _masterSliderInput.text = _stringBuilder.ToString();
    }

    // �}�X�^�[ ���̓t�B�[���h�ł̕ύX
    private void MasterInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_masterSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _masterSliderValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_masterSliderValue >= _VOLUME_MAX)
            _masterSliderValue = _VOLUME_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetMasterVolume((int)_masterSliderValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}", _masterSliderValue);
        _masterSliderInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _masterSlider.value = _masterSliderValue;
    }
    // �}�X�^�[ �X���C�_�[�ł̕ύX
    private void BGMSliderSliderSetting()
    {
        _stringBuilder.Clear();
        _bgmSliderValue = _bgmSlider.value;
        OptionManager.Instance.SetBGMVolume((int)_bgmSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _bgmSliderValue);
        _bgmSliderInput.text = _stringBuilder.ToString();
    }

    // �}�X�^�[ ���̓t�B�[���h�ł̕ύX
    private void BGMInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_bgmSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _bgmSliderValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_bgmSliderValue >= _VOLUME_MAX)
            _bgmSliderValue = _VOLUME_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetBGMVolume((int)_bgmSliderValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}", _bgmSliderValue);
        _bgmSliderInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _bgmSlider.value = _bgmSliderValue;
    }
    // �}�X�^�[ �X���C�_�[�ł̕ύX
    private void SESliderSliderSetting()
    {
        _stringBuilder.Clear();
        _seSliderValue = _seSlider.value;
        OptionManager.Instance.SetSEVolume((int)_seSliderValue);
        _stringBuilder.AppendFormat("{0:F2}", _seSliderValue);
        _seSliderInput.text = _stringBuilder.ToString();
    }

    // �}�X�^�[ ���̓t�B�[���h�ł̕ύX
    private void SEInputFieldSetting()
    {
        float tryValue = 0.0f;
        if (!float.TryParse(_seSliderInput.text, out tryValue)) return;
        _stringBuilder.Clear();
        _seSliderValue = tryValue;

        // ���x����I�[�o�[�΍�
        if (_seSliderValue >= _VOLUME_MAX)
            _seSliderValue = _VOLUME_MAX;

        // ���x�𔽉f
        OptionManager.Instance.SetSEVolume((int)_seSliderValue);
        // �e�L�X�g�ɏ������ʂ܂ŕ\��
        _stringBuilder.AppendFormat("{0:F2}", _seSliderValue);
        _seSliderInput.text = _stringBuilder.ToString();
        // �X���C�_�[�ɔ��f
        _seSlider.value = _seSliderValue;
    }

}
