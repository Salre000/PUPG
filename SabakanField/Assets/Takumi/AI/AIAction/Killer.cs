using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Killer : AIJobBase
{
    /// <summary>
    /// ターゲットのオブジェクト
    /// </summary>
    GameObject targetObject;
    Vector3 targetPos;
    private readonly float EPSILON = 10;
    int LostTimeID;

    public override void Initialize()
    {
        targetObject = AIUtility.GetPlayer();
        LostTimeID = timeID;
        timeID = 1;
    }
    public override void Start()
    {

    }
    public override int GetUniqueID()
    {
        return characterID + (LostTimeID * 4) + 1;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        GoalCheck();
    }

    public override void EmergencyTarget()
    {

    }
    public override void Hit()
    {
        AIUtility.AddDeathCount(characterID + (timeID * 4) + 1);

    }
    protected override List<AIJobBase> GetTagetObject()
    {
        List<AIJobBase> ia=new List<AIJobBase>();
        for(int i=0;i< AICharacterUtility.CharacterCount();i++)
        {
            if (AICharacterUtility.GetAIS()[i].GetAIJob() == this) continue;

            Debug.DrawLine(_gameObject.transform.position, AICharacterUtility.GetAIS()[i].gameObject.transform.position, Color.cyan, 2);

            ia.Add(AICharacterUtility.GetAIS()[i].GetAIJob());
        }

        return ia;
    }

    /// <summary>
    /// このクラスにしかないターゲットを与える関数
    /// </summary>
    /// <param target="これはフラッグを入れる"></param>
    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }
    private void GoalCheck()
    {
        if (Vector3.Distance(_gameObject.transform.position, targetPos) > EPSILON) return;

        SetLoop();
    }

    /// <summary>
    /// 基本使いするナビメッシュの対象を格納する関数
    /// </summary>
    public override bool SetNav()
    {
        if (_agent.hasPath) return false;

        _agent.SetDestination(targetObject.transform.position);
        targetPos=targetObject.transform.position;
        return true;

    }
}
