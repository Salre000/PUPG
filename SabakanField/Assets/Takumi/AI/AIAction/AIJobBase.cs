using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIJobBase
{
    public static readonly Vector3 offSet=new Vector3(0,1.25f,0);

    protected NavMeshAgent _agent;

    protected GameObject _gameObject;

    protected List<System.Action> FixedAction = new List<System.Action>();
    protected List<System.Action> NextFixedAction = new List<System.Action>();

    protected int timeID = -1;

    protected int characterID = -1;

    protected float viewing = 40;

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

        Debug.Log("Count" + FixedAction.Count);


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
    }
    public void EndStop()
    {
        _agent.isStopped = false;
    }

    protected List<AIJobBase> GetTagetObject()
    {

        return AIUtility.GetTeamAIManager(timeID).GetAIS();
    }

    public GameObject GetGameObject() { return _gameObject; }

    private readonly Vector3 RayOffSet = new Vector3(0, 1, 0);
    //“G‚ğ‹”F‚µ‚½‚©‚Ç‚¤‚©
    protected bool CheckTarget()
    {
        //Debag
        if (_agent.isStopped) return false;

        List<AIJobBase> ais = GetTagetObject();

        for (int i = 0; i < ais.Count; i++)
        {
            Vector3 vec = _gameObject.transform.position - ais[i].GetGameObject().transform.position;

            float nowAngle = Vector3.Angle(_gameObject.transform.forward, vec);

            //‹–ìŠp‚È‚¢‚Å‚ ‚é‚±‚Æ‚ªŠm’è
            if (nowAngle > viewing) continue;

            RaycastHit hit;

            if (Physics.Raycast(_gameObject.transform.position + RayOffSet, vec, out hit))
            {

                CharacterInsterface character = hit.transform.gameObject.GetComponent<CharacterInsterface>();
                if (character == null) continue;

                AI ai = hit.transform.GetComponent<AI>();
                if (ai == null) continue;

                if (ai.GetAIJob().GetTimeID() == timeID) continue;

                AIUtility.GetEnemyAI((timeID + 1) % 2)[characterID].ShotReserve(ai.transform.gameObject); ;

                _gameObject.transform.LookAt(hit.transform);

                Stop();

                return true;

            }
            else
            {
                continue;
            }




        }



        return false;
    }

    public void SetMoveSpeed(float speed) { _agent.speed = speed; }

}