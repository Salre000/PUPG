using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Defender : AIJobBase
{
    /// <summary>
    /// ç‚é‚×‚«‘ÎÛ
    /// </summary>
    GameObject defendObject;
    private readonly float EPSILON = 5f;

    Vector3 target=Vector3.zero;

    float Angle = 0;


    public override void Initialize()
    {
        defendObject = AIUtility.GetTeamAIManager((timeID+1)%2).DefenderPos();
    }
    public override void Start()
    {

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(defendObject.name + ":" + timeID);
        GoalCheck();
    }

    public override void EmergencyTarget()
    {

    }

    /// <summary>
    /// ‚±‚ÌƒNƒ‰ƒX‚É‚µ‚©‚È‚¢ç‚é‚×‚«‘ÎÛ‚ğ—^‚¦‚éŠÖ”
    /// </summary>
    public void SetDefendObject(GameObject target)
    {
        defendObject = target;
    }
    public void SetTargetAngle(GameObject target) 
    {
        Vector3 vec = target.transform.position - defendObject.transform.position;

        Angle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

        SetNestTarget();

    }
    private void GoalCheck()
    {
        if (Vector3.Distance(_gameObject.transform.position, target) > EPSILON) return;


        SetNestTarget();


        _agent.SetDestination(target);

        SetLoop();
    }
    private void SetNestTarget() 
    {

        float tragetAngle = (Angle + (Random.Range(0, 85)-40))*Mathf.Deg2Rad;

        float targetRange = Random.Range(CreateMapManager.createMap.GetSIZEX() / 5, CreateMapManager.createMap.GetSIZEX() * 2);


        target = new Vector3(Mathf.Sin(tragetAngle)*targetRange,0, Mathf.Cos(tragetAngle) * targetRange);
        Debug.Log("Ÿ‚ÌêŠ" + target);

    }
    public override bool SetNav()
    {
        if (_agent.hasPath) return false;

        Debug.Log("‘ÎÛ‚ªŒ©‚Â‚©‚ç‚È‚¢");
        _agent.SetDestination(target);

        return true;

    }
}
