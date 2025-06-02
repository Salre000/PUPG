using System;
using UnityEngine;

//時間経過で見えなくなるオブジェクトのクラス
public class DisappearInTime : MonoBehaviour
{
    private　float _timeCount =0;
    private  float _LIMIT_TIME = 4.0f;
    public void SetLimitTime(float time) {  _LIMIT_TIME = time; }

    System.Func<bool> end2Action=()=>true;
    public void SetEndFancs(System.Func<bool> Func) {  end2Action = Func; }

    System.Action endAction;
    public void SetEndActions(System.Action Action) { endAction = Action; }
    public void FixedUpdate()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount < _LIMIT_TIME || !end2Action())
        {

            return;
        }

        if(endAction!=null)endAction();


        _timeCount = 0;

        this.gameObject.SetActive(false);
        
    }
}
