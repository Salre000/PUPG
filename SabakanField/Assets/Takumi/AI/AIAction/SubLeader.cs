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
        CheckGole();
    }

    public override void EmergencyTarget()
    {

    }
    private void CheckGole()
    {
        //Debug.Log("残りの距離" + Vector3.Distance(_attackObject.transform.position, _gameObject.transform.position));
        if (Vector3.Distance(_attackObject.transform.position, _gameObject.transform.position) > 20) return;

        if (timeID == 0)
        {

            Defender defender = new Defender();

            defender.SetObject(_gameObject);

            defender.SetNextFixedAction(() =>
            {
                defender.SetLoop();
            });

            defender.SetID(GetID());
            defender.SetTimeID((GetTimeID() + 1) % 2);
            defender.Initialize();
            defender.SetTimeID(GetTimeID());
            AIUtility.GetEnemyAI((GetTimeID() + 1) % 2)[characterID].Resurrect();

            AIUtility.GetTeamAIManager(timeID).SetJob(characterID, defender);

            _gameObject.GetComponent<AI>().SetAIJob(defender);

            _gameObject.GetComponent<AI>().iJob = PublicEnum.AIJob.defender;

        }
        else
        {

            Killer killer = new Killer();
            killer.SetObject(_gameObject);

            killer.SetNextFixedAction(() =>
            {
                killer.SetLoop();
            });
            AIUtility.GetEnemyAI((GetTimeID() + 1) % 2)[characterID].Resurrect();

            killer.SetID(GetID());
            killer.SetTimeID(GetTimeID());
            killer.Initialize();
            _gameObject.GetComponent<AI>().iJob = PublicEnum.AIJob.kiiler;
            AIUtility.GetTeamAIManager(timeID).SetJob(characterID, killer);

            _gameObject.GetComponent<AI>().SetAIJob(killer);

        }

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
