using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : AIJobBase
{
    /// <summary>
    /// ターゲットのオブジェクト
    /// </summary>
    GameObject readerObject;

    Vector3 targetPos=Vector3.zero;

    private readonly float EPSILON = 5f;

    public override void Initialize()
    {

    }
    public override void Start()
    {

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        GoalCheck(); 
        SetTarget();
    }

    public override void EmergencyTarget()
    {

    }

    /// <summary>
    /// このクラスにしかないリーダーを与える関数
    /// </summary>
    public void SetReaderObject(GameObject target)
    {
        readerObject = target;
    }
    float time = 0;
    private void SetTarget() 
    {
        time = Time.deltaTime;

        if (time < EPSILON) return;
        time = 0;

        SetLoop();


    }

    private void GoalCheck() 
    {
        if (Vector3.Distance(_gameObject.transform.position, targetPos) > EPSILON) return;

        SetLoop();
    }
    public override bool SetNav()
    {
        if (_agent.hasPath) return false;

        _agent.SetDestination(readerObject.transform.position);
        targetPos=readerObject.transform.position;

        return true;

    }
}
