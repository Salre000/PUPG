using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateMap;

public class TeamAIManager
{
    public int ID = -1;

    private List<AIJobBase> Ais = new List<AIJobBase>();
    public void SetJob(int id,AIJobBase aIJob) { Ais[id] = aIJob; }
    [SerializeField]
    private GameObject readerObject;

    public void Initialize(List<AI> characters, int id)
    {
        ID = id;
        SetType();



        PublicEnum.AIJob[] job = SetAIJob(characters.Count);

        for (int i = 0; i < characters.Count; i++)
        {
            AIJobBase jobBase = GetAIJobType(job[i]);

            jobBase.SetObject(characters[i].gameObject);

            jobBase.SetNextFixedAction(() =>
            {
                jobBase.SetLoop();
            });

            jobBase.SetTimeID(ID);
            jobBase.SetID(i);
            jobBase.Initialize();
            jobBase.SetMoveSpeed(GetSpeed(characters[i].GetGanType()));

            Ais.Add(jobBase);

            characters[i].iJob = job[i];

            characters[i].SetAIJob(jobBase);




            if (job[i] != PublicEnum.AIJob.reader) continue;
            readerObject = characters[i].gameObject;
        }
    }

    private void SetType()
    {

        switch (GameModes.mode)
        {
            case PublicEnum.GameMode.flag:
                sbuPos = SetSbu();
                readerPos = SetReader();
                defenderPos = SetDefender();
                break;
            case PublicEnum.GameMode.deathmatch:

                break;
            case PublicEnum.GameMode.spike:
                sbuPos = SetSbuSpaik();
                readerPos = SetReaderSpaik();
                defenderPos = SetDefenderSpaik();
                break;
        }

    }

    /// <summary>
    /// チームメンバー全員に自陣に戻るように促す
    /// </summary>
    public void EmergencyCall()
    {

        sbuPos = AIUtility.GetFlag(ID).transform.position;
        readerPos = AIUtility.GetFlag(ID);
        defenderPos = AIUtility.GetFlag(ID);


        for (int i=0;i< Ais.Count; i++) 
        {
            Ais[i].Initialize();



        }


    }

    private PublicEnum.AIJob[] SetAIJob(int count)
    {
        PublicEnum.AIJob[] aIJob = new PublicEnum.AIJob[count];

        aIJob[0] = PublicEnum.AIJob.defender;
        aIJob[1] = PublicEnum.AIJob.reader;


        for (int i = 2; i < count; i++)
        {
            aIJob[i] = ID==0?(PublicEnum.AIJob)Random.Range((int)PublicEnum.AIJob.member, (int)PublicEnum.AIJob.kiiler): PublicEnum.AIJob.defender;


        }
        if(GameModes.mode!=PublicEnum.GameMode.deathmatch)return aIJob;

        for(int i = 0; i < aIJob.Length; i++) 
        {
            aIJob[i] = PublicEnum.AIJob.kiiler;
        }
        return aIJob;
    }

    private AIJobBase GetAIJobType(PublicEnum.AIJob job)
    {

        switch (job)
        {
            case PublicEnum.AIJob.reader:
                LeaderJob jobBaseL = new LeaderJob();
                return jobBaseL;
            case PublicEnum.AIJob.defender:

                Defender jobBaseD = new Defender();
                jobBaseD.SetDefendObject(AIUtility.GetFlag(ID));
                jobBaseD.SetTargetAngle(AIUtility.GetFlag((ID + 1) % 2));

                return jobBaseD;
            case PublicEnum.AIJob.member:
                Member jobBaseM = new Member();
                jobBaseM.SetReaderObject(readerObject);

                return jobBaseM;
            case PublicEnum.AIJob.subreader:
                SubLeader jobBaseS = new SubLeader();
                jobBaseS.SetReaderObject(AIUtility.GetFlag((ID + 1) % 2));


                return jobBaseS;
            case PublicEnum.AIJob.kiiler:

                return new Killer();

                break;
            default:
                break;
        }

        return null;

    }

    public List<AIJobBase> GetAIS() { return Ais; }

    private float GetSpeed(GanObject.ConstancyGanType type)
    {
        switch (type)
        {
            case GanObject.ConstancyGanType.SL_8:
                return 3.0f;
            case GanObject.ConstancyGanType.Classic:
                return 5f;
            case GanObject.ConstancyGanType.Stechkin:
                return 3.0f;
            case GanObject.ConstancyGanType.FAR_EYE:
                return 1.5f;
            case GanObject.ConstancyGanType.EyeOfHorus:
                return 2f;
        }

        return 1.0f;

    }

    private Vector3 sbuPos;
    public Vector3 sbuReaderPos()
    {
        return sbuPos;
    }
    private GameObject readerPos;
    public GameObject ReaderPos()
    {
        return readerPos;
    }
    private GameObject defenderPos;
    public GameObject DefenderPos()
    {
       return defenderPos;
    }


    private Vector3 SetSbu()
    {
        Vector3 _via = Vector3.zero;
        int Rand = Random.Range(0, 2);

        if (Rand > 0) _via += new Vector3(0, 0, MAP_RETO * CreateMapManager.createMap.GetSIZEX() - 5);
        else _via += new Vector3(MAP_RETO * CreateMapManager.createMap.GetSIZEY() - 5, 0, 0);

        return _via;

    }

    private Vector3 SetSbuSpaik()
    {
        return SpaikMap.Instance.GetStartPos(ID).transform.position;


    }
    private GameObject SetReader()
    {
        return AIUtility.GetFlag((ID + 1) % 2);

    }

    private GameObject SetReaderSpaik()
    {
        return SpaikMap.Instance.GetSpaikArea(ID);


    }
    private GameObject SetDefender()
    {
        return AIUtility.GetFlag(ID);

    }

    private GameObject SetDefenderSpaik()
    {
        return SpaikMap.Instance.GetPoint(ID);


    }

}
