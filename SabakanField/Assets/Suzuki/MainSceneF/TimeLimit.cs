using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeLimit:UIBase
{
    private TextMeshProUGUI _timeLimitText;
    private StringBuilder _stringBuilder = new StringBuilder();
    // ゲーム内で動作する制限時間
    private float _time;                // 全体的な時間
    private float _timeLimit_s = 0.0f;  // 秒
    private float _timeLimit_m = 0.0f;  // 分
    // 制限時間(固定値)
    private readonly float _TIME_LIMIT = 300.0f;

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

    // 時間を減らしていく
    private void CountTimeLimit()
    {
        _stringBuilder.Clear();
        _time-=Time.deltaTime;
        Debug.Log(_time);
        _timeLimit_m = _time / 60.0f;
        _timeLimit_s = 0.0f;
        _stringBuilder.AppendFormat("{0:0}:", _timeLimit_m);
        _stringBuilder.AppendFormat("{0:00}:", _timeLimit_s);
        _timeLimitText.text = _stringBuilder.ToString();
    }
}
