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
    // ��%�ŃN���A�������߂�
    private const float _PAERCENT = 10.0f;
    // �Q�[�W�̒l
    private float _count = 0.0f;
    // �������ԃN���X
    private TimeLimit _timeLimit;
    // �|�[�Y�N���X
    private PauseWindow _pauseWindow;


    // �|�[�Y��ʂ��J������
    private bool _isPause = false;

    // �Q�[�W100%�ł��肠������
    private bool _ClearCheckFlag = false;

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

        // �N���X�Q
        _timeLimit = new TimeLimit();
        _timeLimit.Initialize();
        _pauseWindow = new PauseWindow();
        _pauseWindow.Initialize();

    }

    // �N���X�̎��s����
    private void Execute()
    {
        _timeLimit.Execute();
        _pauseWindow.Execute();
    }

    public bool GageMaxCheck()
    {
        PercentTextChenge();
        // �Q�[�W��100%�ɂȂ�����
        if (_count >= _MAX_FLAG_GAGE)
        {
            _count = _MAX_FLAG_GAGE;
            CheckClearScene();
            return true;
        }

        return false;
    }

    // ��̃Q�[�W�̏㏸
    public void FlagCountGage(float count)
    {
        // �Q�[�W��100%�������̓f�X��Ԃ͑����Ȃ��悤�ɂ���
        if (GageMaxCheck() || PlayerManager.IsPlayerDead()) return;
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
        _playerSideGagePaercentText.text = _stringBuilder.ToString();
    }

    // �N���A��ʑJ�ڃ`�F�b�N
    private void CheckClearScene()
    {
        if (!_ClearCheckFlag)
            GameManager.Instance.GameClearCheck();
        _ClearCheckFlag = true;
    }



    // ���Ԑ؂ꂩ�ǂ����Ԃ�
    public bool GetOverLimitTime() { return _timeLimit.GetOverLimit(); }
    public float GetTime() { return _timeLimit.GetTime(); }
    // �|�[�Y��ʂ��J������
    public void SetPauseWindow() { if (_isPause) _isPause = false; else _isPause = true; }
    // �|�[�Y��ʊJ��ԃ`�F�b�N
    public bool IsPause() { return _isPause; }
}
