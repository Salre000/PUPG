
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
    private bool _LimitFlag = false;
    private bool _overTimeFlag = false;
    // ��������(�Œ�l)
    private readonly float _TIME_LIMIT = 10.0f;

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
    }

    public override void Execute()
    {
        CountTimeLimit();
    }

    // ���Ԃ����炵�Ă���
    private void CountTimeLimit()
    {
        if (!_overTimeFlag)
            _time -= Time.deltaTime;
        OverLimit();
        if(GetOverLimit()) return;
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
                _LimitFlag = true;

            }
            return;
        }
        else
        {

        }
    }

    // ���Ԑ؂ꂩ�ǂ����Ԃ�
    public bool GetOverLimit(){ return _LimitFlag; }
    public float GetTime() { return _time; }

}
