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

    [SerializeField] private AIJobBase _job;

    private AIGan aiGan;

    private AIIK aiIK;

    private float HP;
    private readonly float MAXHP = 100;

    private BoxCollider Thiscollider;

    [SerializeField] private GanObject.ConstancyGanType _constancyGanType;


    public AIStatus GetAIStatus() { return _aiStatus; }
    public void SetAIJob(AIJobBase job) { _job = job; }
    public AIJobBase GetAIJob() { return _job; }
    public void SetAIGan(AIGan gan) { aiGan = gan; }
    public AIGan GetAIGan() { return aiGan; }
    public void SetGanType(ConstancyGanType gan) { _constancyGanType = gan; }
    public ConstancyGanType GetGanType() { return _constancyGanType; }
    public bool Flag;
    public void Awake()
    {
        _aiStatus = new AIStatus();
        _aiStatus.Start(gameObject);
        aiIK = GetComponent<AIIK>();
        HP = MAXHP;
        Thiscollider = GetComponent<BoxCollider>();

        ////Yだけを考える
        aiIK.SetLeftRotate(new Vector3(0, 0, 90));
        aiIK.SetRightRotate(new Vector3(0, 0, -90));

    }

    public void FixedUpdate()
    {
        _job.FixedUpdate();
        Flag = _job.sotp;
    }



    public bool HPFaction(float damage)
    {
        HP -= damage;

        return HP <= 0;
    }

    public void HitAction()
    {
        Respawn();
    }

    //射撃モーションへの移行
    public void ShotReserve(GameObject tragetPos)
    {

        _aiStatus.SetAnimatorTrigger("Shot");
        switch (_constancyGanType)
        {
            case ConstancyGanType.Classic:
            case ConstancyGanType.Stechkin:
                SetHandGunPosition(tragetPos);
                break;
            case ConstancyGanType.SL_8:
            case ConstancyGanType.FAR_EYE:
            case ConstancyGanType.EyeOfHorus:
                SetHandPosition(tragetPos);

                break;
            case ConstancyGanType.Max:
                break;
        }
    }
    //射撃
    public void Shot()
    {
        aiGan.Shot();

    }
    public void EndShot()
    {
        aiIK.SetIK(0);
        _job.EndShot();
    }

    public void MoveStart() { _job.EndStop(); }
    private void SetHandPosition(GameObject tragetPos)
    {
        Vector3 vec = (tragetPos.transform.position) - (transform.position);
        vec.Normalize();
        aiIK.SetIK(1);

        aiIK.SetRightPos(vec / 4f + transform.position + offSet);
        aiIK.SetLeftPos(vec / 2f + transform.position + offSet);

        Debug.DrawLine(transform.position + offSet, transform.position + offSet + vec * 10f, Color.green, 3);


    }
    private void SetHandGunPosition(GameObject tragetPos)
    {
        Vector3 vec = (tragetPos.transform.position) - (transform.position);
        vec.Normalize();

        aiIK.SetIK(1);

        aiIK.SetRightPos(vec / 2f + transform.position + offSet + transform.right / 15f);
        aiIK.SetLeftPos(vec / 2f + transform.position + offSet);

        ////Yだけを考える
        aiIK.SetLeftRotate(new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, 90));
        aiIK.SetRightRotate(new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, -90));

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
        _job.ChengeShoting(true);
        _job.Stop();



    }
    public void Resurrect()
    {
        //座標の移動
        transform.position = AIUtility.GetFlag(_job.GetTimeID()).transform.position;

        Debug.Log(transform.position + "Z:Z" + AIUtility.GetFlag((_job.GetTimeID() + 1) % 2).transform.position);
        _job.ChengeShoting(false);

        _job.EndStop();

    }
    public void ReStart()
    {
        //フラグなどをリセットする
        Thiscollider.enabled = true;

    }


    //Debag
    public PublicEnum.AIJob iJob;

}
