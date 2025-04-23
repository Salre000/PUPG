
using System.Text;
using TMPro;
using UnityEngine;

public class TimeLimit:UIBase
{
    private TextMeshProUGUI _timeLimitText;
    private StringBuilder _stringBuilder = new StringBuilder();
    // ゲーム内で動作する制限時間
    private float _time;                // 全体の時間
    private float _timeLimit_s = 0.0f;  // 秒
    private float _timeLimit_m = 0.0f;  // 分
    private bool _overTimeFlag = false;
    // 制限時間(固定値)
    private readonly float _TIME_LIMIT = 100.0f;

    /// <summary>
    /// デバッグコマンド
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

    // 時間を減らしていく
    private void CountTimeLimit()
    {
        if (!_overTimeFlag)
            _time -= Time.deltaTime;
        OverLimit();
        if(GetOverTime()) return;
        _stringBuilder.Clear();
        _timeLimit_m = _time / 60.0f;
        _timeLimit_s = _time%60.0f;
        // 小数点切り捨てするためint変換
        int result_m=Mathf.FloorToInt( _timeLimit_m );
        int result_s = Mathf.FloorToInt(_timeLimit_s);
        _stringBuilder.AppendFormat("{0}:", result_m);
        _stringBuilder.AppendFormat("{0:00}", result_s);
        _timeLimitText.text = _stringBuilder.ToString();
    }

    // 時間切れ
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

    // _オーバータイムかどうか返す
    public bool GetOverTime(){ return _overTimeFlag; }
    public float GetTime() { return _time; }

}
