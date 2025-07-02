using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAIManager
{
    public int ID = -1;

    private List<AIJobBase> Ais = new List<AIJobBase>();
    [SerializeField]
    private GameObject readerObject;

    public void Initialize(List<AI> characters, int id)
    {
        ID = id;

        PublicEnum.AIJob[] job = SetAIJob(characters.Count);

        for (int i = 0; i < characters.Count; i++)
        {
            AIJobBase jobBase = GetAIJobType(job[i]);

            jobBase.SetObject(characters[i].gameObject);

            jobBase.SetNextFixedAction(() =>
            {
                jobBase.SetLoop();
            });

            jobBase.Initialize();
            jobBase.SetTimeID(ID);
            jobBase.SetID(i);
            jobBase.SetMoveSpeed(GetSpeed(characters[i].GetGanType()));

            Ais.Add(jobBase);

            characters[i].iJob = job[i];

            characters[i].SetAIJob(jobBase);




            if (job[i] != PublicEnum.AIJob.reader) continue;
            readerObject = characters[i].gameObject;
        }
    }


    /// <summary>
    /// チームメンバー全員に自陣に戻るように促す
    /// </summary>
    public void EmergencyCall()
    {



    }

    private PublicEnum.AIJob[] SetAIJob(int count)
    {
        PublicEnum.AIJob[] aIJob = new PublicEnum.AIJob[count];

        aIJob[0] = PublicEnum.AIJob.defender;
        aIJob[1] = PublicEnum.AIJob.reader;


        for (int i = 2; i < count; i++)
        {
            aIJob[i] = (PublicEnum.AIJob)Random.Range((int)PublicEnum.AIJob.member, (int)PublicEnum.AIJob.Max);


        }

        return aIJob;

    }

    private AIJobBase GetAIJobType(PublicEnum.AIJob job)
    {

        switch (job)
        {
            case PublicEnum.AIJob.reader:
                LeaderJob jobBaseL = new LeaderJob();

                jobBaseL.SetTargetObject(AIUtility.GetFlag((ID + 1) % 2));

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


}
