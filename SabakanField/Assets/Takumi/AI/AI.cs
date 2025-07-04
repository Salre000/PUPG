using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static AIJobBase;
using static GanObject;

public class AI : MonoBehaviour, CharacterInsterface, InvincibleInsterface
{

    [SerializeField] public GameObject leftHand;
    [SerializeField] public GameObject rightHand;

    private AIStatus _aiStatus;

    [SerializeField] private AIJobBase _job;

    private AIGan aiGan;

    private AIIK aiIK;

    private Outline outline;

    [SerializeField]private float HP;
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
        outline=GetComponent<Outline>();
        outline.enabled = false;
        ////Yだけを考える
        aiIK.SetLeftRotate(new Vector3(0, 0, 90));
        aiIK.SetRightRotate(new Vector3(0, 0, -90));

    }

    public void FixedUpdate()
    {
        _job.FixedUpdate();
    }



    public bool HPFaction(float damage)
    {
        HP -= damage;

        return HP <= 0;
    }

    public void HitAction(GameObject Enemy = null)
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
        aiGan.Shot(_job.characterID + (_job.timeID * 4));

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
        gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, 0);
        aiIK.SetIK(1);

        aiIK.SetRightPos(vec / 4f + transform.position + offSet);
        aiIK.SetLeftPos(vec / 2f + transform.position + offSet);



    }
    private void SetHandGunPosition(GameObject tragetPos)
    {
        Vector3 vec = (tragetPos.transform.position) - (transform.position);
        vec.Normalize();

        aiIK.SetIK(1);
        gameObject.transform.eulerAngles = new Vector3(0, Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg, 0);

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
        _job.ChengeShoting(true);
        _job.Stop();
        Debug.Log("死んだ");


    }
    public void Resurrect()
    {
        //座標の移動
        transform.position = AIUtility.GetFlag(_job.GetTimeID()).transform.position;

        _job.ChengeShoting(false);

        HP = MAXHP;

    }
    public void ReStart()
    {
        //フラグなどをリセットする
        Thiscollider.enabled = true;
        _job.EndStop();


    }

    public bool GetISLife() 
    {
        return HP > 0;
    }
    public void ChengeOuutLIne(bool flag) 
    {
        outline.enabled = flag;

    }

    //Debag
    public PublicEnum.AIJob iJob;

}
