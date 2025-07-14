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
        targetObject = AIUtility.GetTeamAIManager((timeID+1)%2).ReaderPos();
    }
    public override void Start()
    {

    }
    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        CheckGole();
    }

    public override void EmergencyTarget()
    {

    }
    private void CheckGole() 
    {
        if (Vector3.Distance(targetObject.transform.position, _gameObject.transform.position) > 14) return;

        if (timeID == 0) 
        {

            Defender defender=new Defender();

            defender.SetObject(_gameObject);

            defender.SetNextFixedAction(() =>
            {
                defender.SetLoop();
            });

            defender.SetID(GetID());
            defender.SetTimeID((GetTimeID()+1)%2);
            defender.Initialize();
            AIUtility.GetEnemyAI((GetTimeID()+1)%2)[characterID].Resurrect();

            defender.SetTargetAngle(AIUtility.GetFlag(GetTimeID()));
            defender.SetTimeID(GetTimeID());
            defender.SetLoop();
            _gameObject.GetComponent<AI>().SetAIJob(defender);
            AIUtility.GetTeamAIManager(timeID).SetJob(characterID, defender);

            _gameObject.GetComponent<AI>().iJob = PublicEnum.AIJob.defender;

        }
        else 
        {

            Killer killer =new Killer();
            killer.SetObject(_gameObject);

            killer.SetNextFixedAction(() =>
            {
                killer.SetLoop();
            });

            killer.SetID(GetID());
            killer.SetTimeID(GetTimeID());
            killer.Initialize();
            AIUtility.GetEnemyAI((GetTimeID() + 1) % 2)[characterID].Resurrect();
            _gameObject.GetComponent<AI>().iJob = PublicEnum.AIJob.kiiler;
            AIUtility.GetTeamAIManager(timeID).SetJob(characterID, killer);

            _gameObject.GetComponent<AI>().SetAIJob(killer);

        }

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
