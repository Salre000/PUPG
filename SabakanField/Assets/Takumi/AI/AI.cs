using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GanObject;

public class AI : MonoBehaviour, CharacterInsterface
{
    [SerializeField] public GameObject leftHand;
    [SerializeField] public GameObject rightHand;


    private AIStatus _aiStatus;

    [SerializeField]private AIJobBase _job;

    private AIGan aiGan;

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
    }

    public void FixedUpdate()
    {
        _job.FixedUpdate();
    }



    public bool HPFaction(float damage)
    {
        return false;
    }

    public void HitAction()
    {
    }

    public void ShotReserve() 
    {
        
        _aiStatus.SetAnimatorTrigger("Shot");
    }
    public void Shot() 
    {
        aiGan.Shot(transform.eulerAngles.y);
        
    }
    public void EndShot() 
    {
        _job.EndStop();
    }



    //Debag
    public PublicEnum.AIJob iJob;

}
