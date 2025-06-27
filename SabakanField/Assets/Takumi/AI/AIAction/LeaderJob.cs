using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderJob : AIJobBase
{
    /// <summary>
    /// ターゲットのオブジェクト
    /// </summary>
    GameObject targetObject;


    public override void Initialize()
    {

    }
    public override void Start()
    {

    }
    public override void FixedUpdate() 
    {
        base.FixedUpdate();

    }

    public override void EmergencyTarget()
    {

    }

    /// <summary>
    /// このクラスにしかないターゲットを与える関数
    /// </summary>
    /// <param target="これはフラッグを入れる"></param>
    public void SetTargetObject(GameObject target) 
    {
        targetObject=target;
    }

    /// <summary>
    /// 基本使いするナビメッシュの対象を格納する関数
    /// </summary>
    public override bool SetNav()
    {
        if (_agent.hasPath) return false;

        _agent.SetDestination(targetObject.transform.position);

        return true;

    }
}
