using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static GanObject;

public class AIManager : MonoBehaviour
{

    //プレイヤーの味方のAIのリスト
    List<AI> players = new List<AI>();

    //プレイヤーの味方のAIの生存状況のリスト
    [SerializeField] List<bool> playersLife = new List<bool>();

    //敵のAIのリスト
    List<AI> enemys = new List<AI>();

    //敵のAI生存状況状況
    [SerializeField] List<bool> enemyLife = new List<bool>();

    //フラッグのオブジェクトの配列
    [SerializeField] GameObject[] flagObject = new GameObject[2];

    //フラッグのオブジェクトの色を変更するマテリアルの配列
    [SerializeField] private Material[] color = new Material[2];

    //AIのオリジナルオブジェクト
    [SerializeField] GameObject origenAI;

    [SerializeField] private TeamAIManager playerTeamAIManager;
    private TeamAIManager enemyTeamAIManager;

    public TeamAIManager GetEnemyTime(int index) { return index == 0 ? enemyTeamAIManager : playerTeamAIManager; }
    public List<AI> GetEnemyAI(int index) { return index == 0 ? enemys : players; }


    [SerializeField, Header("ハンドガンの銃オーバライドのアニメーション")] AnimatorOverrideController HandGanType;


    //1つの陣営のAIの数（プレイヤー側は-１）
    public const int AI_NUMBER = 5;

    private readonly float FLAG_PLAYER_RENGE = 3;

    //プレイヤーの格納先
    [SerializeField] public GameObject player;

    public GameObject GetPlayer() { return player; }
    public static KIllCount kIll;

    public List<int> GetKillCount() { return kIll.killCount; }
    public List<int> GetDeathCount() { return kIll.deathCount; }

    public void AddDeathCount(int index) { kIll.deathCount[index]++;}
    //終了切る数
    private readonly int KILLMAX = 30;
    public void AdDKillCount(int index) { kIll.killCount[index]++; if (kIll.killCount[index] >= KILLMAX) GameManager.Instance.GameClearCheck(); }
    public void AddAssertCount(int index) { kIll.assistCount[index]++; }


    public List<bool> GetPlayersLife() { return playersLife; }
    public List<bool> GetEnemyLife() { return enemyLife; }
    public GameObject GetFlag(int ID) { return flagObject[ID]; }

    public List<GameObject> GetchracterALL()
    {
        List<GameObject> list = new List<GameObject>();

        list.Add(player);

        for (int i = 0; i < players.Count; i++)
        {
            list.Add(players[i].gameObject);
        }
        for (int i = 0; i < enemys.Count; i++)
        {
            list.Add(enemys[i].gameObject);
        }

        return list;

    }

    private int IDNumber = 0;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void FixedUpdate()
    {
        ScanAILife();
        Debug();
    }
    public int GetID()
    {
        int number = IDNumber;
        IDNumber++;
        return number;
    }

    public Vector3 PlayerFlagPosition() { return FlagPos[0]; }

    public void DataSave()
    {
        KillData.InGameDataSave(kIll);
    }





    private void ScanAILife()
    {
        playersLife.Clear();
        players.Capacity = players.Capacity;



        for (int i = 0; i < players.Count; i++)
        {
            playersLife.Add(AICharacterUtility.GetAIS()[i].GetISLife());

        }

        enemyLife.Clear();
        enemyLife.Capacity = enemys.Capacity;
        for (int i = 0; i < enemys.Count; i++)
        {
            enemyLife.Add(AICharacterUtility.GetAIS()[i + 4].GetISLife());

        }


    }

    public void SetFlagObject(GameObject flagObject, int number)
    {
        this.flagObject[number] = flagObject;
    }

    private bool one = false;
    private Vector3[] FlagPos = { CreateMap._PLAYERFLAG_POSITION, CreateMap._ENEMYFLAG_POSITION };
    public void CreateAI()
    {
        if (one) return;
        FlagPos[0] = CreateMap._PLAYERFLAG_POSITION; FlagPos[1] = CreateMap._ENEMYFLAG_POSITION;

        kIll = new KIllCount();

        kIll.players.Clear();

        kIll.Name = kIll.GetRandomName();


        kIll.killCount.Clear();
        kIll.deathCount.Clear();
        kIll.assistCount.Clear();

        AICharacterUtility.ClearCharacterAI();

        kIll.killCount.Add(0);
        kIll.deathCount.Add(0);
        kIll.assistCount.Add(0);

        AICharacterUtility.ResetAI();

        GanObject.LoodGameObject();

        GunSoundManager.Initialize();





        for (int i = 0; i < FlagPos.Length; i++)
        {

            Vector3 vec = FlagPos[(i + 1) % 2] - FlagPos[i];

            float angle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

            for (int j = 0; j < AI_NUMBER; j++)
            {
                if (i == 0 && j == 0) continue;
                GameObject ai = GameObject.Instantiate(origenAI);

                float createAngle = angle + (30 * j) - 60;
                RaandomGan(ai);


                ai.transform.eulerAngles = new Vector3(0, createAngle, 0);

                ai.transform.position = FlagPos[i]
                +new Vector3(Mathf.Sin(createAngle * Mathf.Deg2Rad), 0,
                Mathf.Cos(createAngle * Mathf.Deg2Rad)) * FLAG_PLAYER_RENGE;
                if (GameModes.mode == PublicEnum.GameMode.deathmatch)
                {

                    int mapMax = CreateMapManager.GetMAPMAXSIZE() * 5 - 1;

                    Vector2 mapReta = CreateMapManager.GetMapRate();


                    createAngle = 36 * ((i * 5) + j);
                    ai.transform.eulerAngles = new Vector3(0, createAngle + 180, 0);

                    ai.transform.position = new Vector3(Mathf.Sin(createAngle) * mapMax, 0, Mathf.Cos(createAngle) * mapMax);

                    ai.transform.position += new Vector3((mapReta.x * 10 - 10) / 2, 0, (mapReta.y * 10 - 10) / 2);



                }
                ai.transform.name += ((i * 5) + j).ToString();


                AI Ai = ai.GetComponent<AI>();
                AICharacterUtility.AddAI(Ai);

                kIll.killCount.Add(0);
                kIll.deathCount.Add(0);
                kIll.assistCount.Add(0);


                if (i <= 0)
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[0];

                    players.Add(Ai);

                }
                else
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[1];

                    enemys.Add(Ai);
                }

            }

        }

        playerTeamAIManager = new TeamAIManager();
        enemyTeamAIManager = new TeamAIManager();

        enemyTeamAIManager.Initialize(enemys, 1);
        playerTeamAIManager.Initialize(players, 0);

        one = true;


    }
    private void Debug()
    {
        //if (Input.GetKey(KeyCode.Alpha0)) players[0].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha1)) players[1].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha2)) players[2].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha3)) players[3].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha4)) enemys[0].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha5)) enemys[1].gameObject.GetComponent<CharacterInsterface>().HitAction();
        //if (Input.GetKey(KeyCode.Alpha6)) enemys[2].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha7)) enemys[3].Resurrect();
        //if (Input.GetKey(KeyCode.Alpha8)) enemys[4].gameObject.GetComponent<AI>().Shot();



    }

    private void RaandomGan(GameObject ai)
    {
        GanObject.ConstancyGanType type = (ConstancyGanType)Random.Range(0, (int)GanObject.ConstancyGanType.Max - 1);
        Animator animator = ai.GetComponent<Animator>();
        int randomRenge = 0;

        GameObject gan = GameObject.Instantiate(GanObject.enemyConstancyGan.objects[(int)type]);
        gan.transform.parent = ai.transform;

        AI aI = ai.GetComponent<AI>();
        aI.SetGanType(type);
        WeaponEquipment weapon = gan.AddComponent<WeaponEquipment>();


        switch (type)
        {
            case ConstancyGanType.SL_8:
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("MoveSpped", 1);
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("ShotSpped", 2);
                randomRenge = 5;
                break;
            case ConstancyGanType.Classic:
                animator.runtimeAnimatorController = HandGanType;
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("MoveSpped", 1f);
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("ShotSpped", 2);
                weapon.handgunFlag = true;
                randomRenge = 7;

                break;
            case ConstancyGanType.Stechkin:
                animator.runtimeAnimatorController = HandGanType;
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("MoveSpped", 1);
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("ShotSpped", 0.5f);
                randomRenge = 3;
                weapon.handgunFlag = true;

                break;
            case ConstancyGanType.FAR_EYE:
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("MoveSpped", 0.5f);
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("ShotSpped", 0.3f);
                randomRenge = 0;
                break;
            case ConstancyGanType.EyeOfHorus:
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("MoveSpped", 0.8f);
                ai.GetComponent<AI>().GetAIStatus().SetAnimatorFloat("ShotSpped", 0.8f);
                randomRenge = 10;
                break;
        }




        weapon.SetLefthand(aI.leftHand);
        weapon.SetRighthand(aI.rightHand);

    }

    public List<AI> GetAIS() { List<AI> chracters = players; for (int i = 0; i < enemys.Count; i++) chracters.Add(enemys[i]); return chracters; }

    public void SetPlayer() { player = GameObject.FindGameObjectWithTag("Player"); }

}

[System.Serializable]
public class KIllCount
{

    public List<string> Name = new List<string>(AIManager.AI_NUMBER * 2) { "Test" };
    public List<int> deathCount = new List<int>(AIManager.AI_NUMBER * 2) { 0 };
    public List<int> killCount = new List<int>(AIManager.AI_NUMBER * 2) { 0 };
    public List<int> assistCount = new List<int>(AIManager.AI_NUMBER * 2) { 0 };
    public List<bool> players = new List<bool>(AIManager.AI_NUMBER * 2) { true };


    //決め打ち２０
    public List<string> NameType = new List<string>(20)
    { "Alex Storm","Isabelle Valkyrie","Marco Falcon","Lisa Shadow","Andrei Ice","Sophia Blaze","Lucas Thunder","Emily Fox","Jean Crow","Oscar Houn","Antonio Raven","Natalie Sky"
    , "Michael Storm","Ana Shadow","Ethan Blaze","Maria Fox","Alexander Ice","Sarah Raven","Daniel Hound","Eve Thunder"};

    public List<string> GetRandomName()
    {
        List<string> name = new List<string>();
        List<int> index = new List<int>();

        players.Add(true);

        for (int i = 0; i < 9; i++)
        {
            players.Add(GameModes.mode == PublicEnum.GameMode.flag ? i < 5 : false);
            int random = Random.Range(0, 20);

            if (index.Contains(random)) { i--; continue; }

            name.Add(NameType[random]);
        }


        return name;

    }


}