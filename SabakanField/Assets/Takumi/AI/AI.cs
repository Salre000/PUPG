using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GanObject;
using static AIJobBase;

public class AI : MonoBehaviour, CharacterInsterface, InvincibleInsterface
{
    [SerializeField] public GameObject leftHand;
    [SerializeField] public GameObject rightHand;

    private AIStatus _aiStatus;

    [SerializeField]private AIJobBase _job;

    private AIGan aiGan;

    private AIIK aiIK;

    private float HP;
    private readonly float MAXHP = 100;

    private BoxCollider Thiscollider;

    [SerializeField]private GanObject.ConstancyGanType _constancyGanType;


    public AIStatus GetAIStatus() { return _aiStatus; }
    public void SetAIJob(AIJobBase job) { _job = job; }
    public AIJobBase GetAIJob() { return _job; }
    public void SetAIGan(AIGan gan) { aiGan = gan; }
    public AIGan GetAIGan() { return aiGan; }
    public void SetGanType(ConstancyGanType gan) { _constancyGanType = gan; }
    public ConstancyGanType GetGanType() { return _constancyGanType; }

    public void Awake()
    {
        _aiStatus = new AIStatus();
        _aiStatus.Start(gameObject);
        aiIK=GetComponent<AIIK>();
        HP=MAXHP;
        Thiscollider=GetComponent<BoxCollider>();
    }

    public void FixedUpdate()
    {
        _job.FixedUpdate();
    }



    public bool HPFaction(float damage)
    {
        HP-=damage;

        return HP<=0;
    }

    public void HitAction()
    {
        Respawn();
    }

    //射撃モーションへの移行
    public void ShotReserve(GameObject tragetPos) 
    {
        
        _aiStatus.SetAnimatorTrigger("Shot");
        SetHandPosition(tragetPos);
    }
    //射撃
    public void Shot() 
    {
        aiGan.Shot();
        
    }
    public void EndShot() 
    {
        aiIK.SetIK(0);
        _job.EndStop();
    }
    private void SetHandPosition(GameObject tragetPos) 
    {
        Vector3 vec = (tragetPos.transform.position) - (transform.position);
        vec.Normalize();
        aiIK.SetIK(1);

        aiIK.SetRightPos(vec / 4f+transform.position + offSet);
        aiIK.SetLeftPos(vec / 2f+transform.position + offSet);

        Debug.DrawLine(transform.position + offSet, transform.position + offSet + vec * 10f,Color.green,3);


    }

    public bool GetInvincibleFlag()
    {
        return false;
    }

    private void Respawn() 
    {
        //アニメーションを替える
        aiIK.SetIK(0);
        _aiStatus.SetAnimatorTrigger("Death");
        Thiscollider.enabled = false;
        HP = MAXHP;

        _job.Stop();



    }
    public void Resurrect() 
    {
        //座標の移動

        _job.EndStop();
        transform.position = AIUtility.GetFlag(_job.GetTimeID()).transform.position;

    }
    public void ReStart() 
    {
        //フラグなどをリセットする
        Thiscollider.enabled = true;

    }


    //Debag
    public PublicEnum.AIJob iJob;

}
