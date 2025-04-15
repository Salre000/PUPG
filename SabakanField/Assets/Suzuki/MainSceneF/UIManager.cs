using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI�Ǘ�

    public static UIManager Instance;
    // �Q�[�W
    private Image _playerSideGageImage;
    // ��%�m�ۂł��Ă��邩�̕\�L
    private TextMeshProUGUI _playerSideGagePaercentText;
    private StringBuilder _stringBuilder = new StringBuilder();
    // �p�[�Z���g�\��
    private const float _MAX_FLAG_GAGE = 1.0f;

    private float _count = 0.0f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        Initialize();
    }

    private void Initialize()
    {
        _playerSideGageImage = GameObject.Find("PlayerSideGageImage").GetComponent<Image>();
        _playerSideGageImage.fillAmount = 0.0f;
        _stringBuilder.Clear();
        _stringBuilder.AppendFormat("{0:0.0}%", _count);
        _playerSideGagePaercentText = GameObject.Find("PercentText").GetComponent<TextMeshProUGUI>();
        _playerSideGagePaercentText.text = _stringBuilder.ToString();
    }

    public bool GageMaxCheck()
    {
        PercentTextChenge();
        // �Q�[�W��100%�ɂȂ�����
        if (_count >= _MAX_FLAG_GAGE)
        {
            _count = _MAX_FLAG_GAGE;
            return true;
        }

        return false;
    }

    // ��̃Q�[�W�̏㏸
    public void FlagCountGage(float count)
    {
        if (GageMaxCheck())
            return;
        _count = count / 100.0f;
        // �Q�[�W�̏㏸

        _playerSideGageImage.fillAmount = _count;
    }

    // �Q�[�W���ǂꂾ�����߂������F�ł���悤��
    private void PercentTextChenge()
    {

        _stringBuilder.Clear();
        float count = _count * 100.0f;
        _stringBuilder.AppendFormat("{0:0.0}%", count);
        _playerSideGagePaercentText.text =_stringBuilder.ToString();
    }
}
