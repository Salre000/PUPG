using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateMap;
public class SubLeader : AIJobBase
{
    /// <summary>
    /// 守るべき対象
    /// </summary>
    GameObject _attackObject;

    private readonly float EPSILON = 5f;

    Vector3 _via;

    Vector3 target = Vector3.zero;

    public override void Initialize()
    {
        _via = AIUtility.GetTeamAIManager(timeID).sbuReaderPos();

        target = _via;

    }
    public override void Start()
    {

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        GoalCheck();
    }

    public override void EmergencyTarget()
    {

    }

    /// <summary>
    /// このクラスにしかな最終攻撃対象と経由地点を与える関数
    /// </summary>
    public void SetReaderObject(GameObject target)
    {
        _attackObject = target;
    }
    private void GoalCheck()
    {
        if (Vector3.Distance(_gameObject.transform.position, target) > EPSILON) return;
        target = _attackObject.transform.position;

        _agent.SetDestination(target);

        SetLoop();
    }

    public override bool SetNav()
    {
        if (_agent.hasPath) return false;

        _agent.SetDestination(target);

        Debug.Log("ターゲット" + target);

        return true;

    }
}
