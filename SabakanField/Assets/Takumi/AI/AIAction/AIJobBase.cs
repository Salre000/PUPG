using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIJobBase
{
    public static readonly Vector3 offSet = new Vector3(0, 1.25f, 0);

    protected NavMeshAgent _agent;

    protected GameObject _gameObject;

    protected List<System.Action> FixedAction = new List<System.Action>();
    protected List<System.Action> NextFixedAction = new List<System.Action>();

    public int timeID = -1;

    public int characterID = -1;

    protected float viewing = 40;

    protected bool shoting=false;

    public bool sotp = false;   

    public void ChengeShoting(bool flag) {shoting = flag;}

    public virtual int GetUniqueID() {return (characterID+timeID*4)+1;}

    public virtual void FixedUpdate()
    {
        for (int i = 0; i < FixedAction.Count; i++) FixedAction[i]();

        CheckTarget();


        FixedAction.Clear();
        for (int i = 0; i < NextFixedAction.Count; i++)
        {
            System.Action action = NextFixedAction[i];

            FixedAction.Add(action);
        }
        NextFixedAction.Clear();


    }
    public virtual void Start()
    {

    }
    public virtual void Initialize()
    {

    }

    /// <summary>
    /// e‚Ì‰Šúİ’è‚È‚Ç‚ğs‚¤ƒNƒ‰ƒX
    /// </summary>
    public void SetGun()
    {

    }
    /// <summary>
    /// ‘S•”‚ÌƒWƒ‡ƒu‹¤’Ê‚Å•K—v‚È©•ª©g‚ğ—^‚¦‚éŠÖ”
    /// </summary>
    /// <param name="game"></param>
    public void SetObject(GameObject game)
    {
        _gameObject = game;

        _agent = _gameObject.GetComponent<NavMeshAgent>();
    }

    public void SetNextFixedAction(System.Action action) { NextFixedAction.Add(action); }

    /// <summary>
    /// ‹Ù‹}‚Å”rœ‚µ‚È‚¯‚ê‚Î‚¢‚¯‚È‚¢“G‚ğ—^‚¦‚é
    /// </summary>
    public abstract void EmergencyTarget();

    public abstract bool SetNav();

    public void SetLoop()
    {
        if (!SetNav()) return;

        SetNextFixedAction(() => { SetLoop(); });
    }
    public void SetTimeID(int id) { timeID = id; }
    public int GetTimeID() { return timeID; }
    public void SetID(int id) { characterID = id; }
    public int GetID() { return characterID; }

    public void Stop()
    {
        _agent.isStopped = true;
        sotp = true;
    }
    public void EndStop()
    {
        sotp = false;
        _agent.isStopped = false;

    }
    public void EndShot()
    {
        //sotp = false;

    }

    protected virtual List<AIJobBase> GetTagetObject()
    {

        return AIUtility.GetTeamAIManager(timeID).GetAIS();
    }

    public GameObject GetGameObject() { return _gameObject; }

    private readonly Vector3 RayOffSet = new Vector3(0, 1, 0);
    //“G‚ğ‹”F‚µ‚½‚©‚Ç‚¤‚©
    protected bool CheckTarget()
    {
        if (sotp) return false;
        Vector3 vec;
        float nowAngle;
        RaycastHit hit;


        List<AIJobBase> ais = GetTagetObject();

        for (int i = 0; i < ais.Count; i++)
        {
           vec = ais[i].GetGameObject().transform.position - _gameObject.transform.position;

            nowAngle = Vector3.Angle(_gameObject.transform.forward, vec);

            //‹–ìŠp‚È‚¢‚Å‚ ‚é‚±‚Æ‚ªŠm’è
            if (nowAngle > viewing) continue;


            if (Physics.Raycast(_gameObject.transform.position + RayOffSet, vec, out hit))
            {


                CharacterInsterface character = hit.transform.gameObject.GetComponent<CharacterInsterface>();
                if (character == null) continue;

                AI ai = hit.transform.GetComponent<AI>();
                if (ai == null) continue;

                if (ai.GetAIJob().GetTimeID() == timeID) continue;

                if (Vector3.Distance(hit.point, _gameObject.transform.position) > 6) continue;

                AIUtility.GetEnemyAI((timeID + 1) % 2)[characterID].ShotReserve(ai.transform.gameObject); ;

                Debug.Log("Shot" + timeID + "*" + characterID);

                               
                Stop();
                return true;

            }
            else
            {
                continue;
            }




        }

        if (timeID == 0) return false;
        vec = AIUtility.GetPlayer().transform.position - _gameObject.transform.position;

       nowAngle = Vector3.Angle(_gameObject.transform.forward, vec);

        //‹–ìŠp‚È‚¢‚Å‚ ‚é‚±‚Æ‚ªŠm’è
        if (nowAngle > viewing) return false;


        if (Physics.Raycast(_gameObject.transform.position + RayOffSet, vec, out hit))
        {


            CharacterInsterface character = hit.transform.gameObject.GetComponent<CharacterInsterface>();
            if (character == null) return false;
            AIUtility.GetEnemyAI((timeID + 1) % 2)[characterID].ShotReserve(hit.transform.gameObject); ;

            Stop();
            shoting = true;

            return true;

        }



            return false;


    }
    public virtual void Hit() 
    {

        AIUtility.AddDeathCount(characterID + (timeID * 4) + 1);



    }
    public void SetMoveSpeed(float speed) { _agent.speed = speed; }

}