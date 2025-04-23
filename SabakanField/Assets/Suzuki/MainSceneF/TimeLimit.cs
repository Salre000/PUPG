
using System.Text;
using TMPro;
using UnityEngine;

public class TimeLimit:UIBase
{
    private TextMeshProUGUI _timeLimitText;
    private StringBuilder _stringBuilder = new StringBuilder();
    // �Q�[�����œ��삷�鐧������
    private float _time;                // �S�̂̎���
    private float _timeLimit_s = 0.0f;  // �b
    private float _timeLimit_m = 0.0f;  // ��
    private bool _overTimeFlag = false;
    // ��������(�Œ�l)
    private readonly float _TIME_LIMIT = 100.0f;

    /// <summary>
    /// �f�o�b�O�R�}���h
    /// </summary>
    private bool _DebugTimeStopFlag = false;

     public override void Initialize()
    {
        _stringBuilder.Clear();
        _timeLimitText = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
        _time=_TIME_LIMIT;
        _timeLimit_m = _TIME_LIMIT / 60.0f;
        _timeLimit_s = 0.0f;
        _stringBuilder.AppendFormat("{0:0}:", _timeLimit_m);
        _stringBuilder.AppendFormat("{0:00}:", _timeLimit_s);
        _timeLimitText.text = _stringBuilder.ToString();

        _DebugTimeStopFlag = false;
    }

    public override void Execute()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            if(!_DebugTimeStopFlag)
                _DebugTimeStopFlag=true;
            else
                _DebugTimeStopFlag=false;
        }
        if (_DebugTimeStopFlag) return;
        CountTimeLimit();
    }

    // ���Ԃ����炵�Ă���
    private void CountTimeLimit()
    {
        if (!_overTimeFlag)
            _time -= Time.deltaTime;
        OverLimit();
        if(GetOverTime()) return;
        _stringBuilder.Clear();
        _timeLimit_m = _time / 60.0f;
        _timeLimit_s = _time%60.0f;
        // �����_�؂�̂Ă��邽��int�ϊ�
        int result_m=Mathf.FloorToInt( _timeLimit_m );
        int result_s = Mathf.FloorToInt(_timeLimit_s);
        _stringBuilder.AppendFormat("{0}:", result_m);
        _stringBuilder.AppendFormat("{0:00}", result_s);
        _timeLimitText.text = _stringBuilder.ToString();
    }

    // ���Ԑ؂�
    public void OverLimit()
    {
        if (_time < 0.0f)
        {
            _time = 0.0f;
            if (!GameManager.Instance.GetFlagRangeCheck())
            {
                _overTimeFlag = true;
            }
            return;
        }
        else
        {

        }
    }

    // _�I�[�o�[�^�C�����ǂ����Ԃ�
    public bool GetOverTime(){ return _overTimeFlag; }
    public float GetTime() { return _time; }

}
