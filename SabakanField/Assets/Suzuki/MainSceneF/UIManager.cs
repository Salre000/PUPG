using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    // ��%�ŃN���A�������߂�
    private const float _PAERCENT = 10.0f;
    // �Q�[�W�̒l
    private float _count = 0.0f;

    // �������ԃN���X
    private TimeLimit _timeLimit;

    

    private bool _DebugClearCheck = false;
    private bool _DebugdefeatCheck = false;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);

        Initialize();
    }

    private void Update()
    {
        Execute();
    }

    private void Initialize()
    {
        _playerSideGageImage = GameObject.Find("PlayerSideGageImage").GetComponent<Image>();
        _playerSideGageImage.fillAmount = 0.0f;
        _stringBuilder.Clear();
        _stringBuilder.AppendFormat("{0:0.0}%", _count);
        _playerSideGagePaercentText = GameObject.Find("PercentText").GetComponent<TextMeshProUGUI>();
        _playerSideGagePaercentText.text = _stringBuilder.ToString();

        _timeLimit = new TimeLimit();
        _timeLimit.Initialize();

    }

    private void Execute()
    {
        _timeLimit.Execute();
    }

    public bool GageMaxCheck()
    {
        PercentTextChenge();
        // �Q�[�W��100%�ɂȂ�����
        if (_count >= _MAX_FLAG_GAGE)
        {
            _count = _MAX_FLAG_GAGE;
            DebugGoClearSceneCheck();
            return true;
        }

        return false;
    }

    // ��̃Q�[�W�̏㏸
    public void FlagCountGage(float count)
    {
        if (GageMaxCheck())
            return;
        _count = count / _PAERCENT;
        // �Q�[�W�̏㏸

        _playerSideGageImage.fillAmount = _count;
    }

    // �Q�[�W���ǂꂾ�����߂������F�ł���悤��
    private void PercentTextChenge()
    {

        _stringBuilder.Clear();
        float count = _count * _PAERCENT;
        _stringBuilder.AppendFormat("{0:0.0}%", count);
        _playerSideGagePaercentText.text =_stringBuilder.ToString();
    }



    // �f�o�b�O�p�N���A��ʑJ�ڃ`�F�b�N
    private void DebugGoClearSceneCheck()
    {
        if(!_DebugClearCheck)
        GameManager.Instance.GameClearCheck();
        _DebugClearCheck=true;
    }

    // ���Ԑ؂ꂩ�ǂ����Ԃ�
    public bool GetOverLimitTime() { return _timeLimit.GetOverLimit(); }
}
