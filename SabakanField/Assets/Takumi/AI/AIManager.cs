using System.Collections;
using System.Collections.Generic;
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
    GameObject[] flagObject = new GameObject[2];

    //フラッグのオブジェクトの色を変更するマテリアルの配列
    [SerializeField] private Material[] color = new Material[2];

    //AIのオリジナルオブジェクト
    [SerializeField] GameObject origenAI;


    [SerializeField, Header("ハンドガンの銃オーバライドのアニメーション")] AnimatorOverrideController HandGanType;


    //1つの陣営のAIの数（プレイヤー側は-１）
    public const int AI_NUMBER = 5;

    //プレイヤーの格納先
    GameObject player;
    KIllCount kIll;

    public List<int> GetKillCount() { return kIll.killCount; }
    public List<int> GetDeathCount() { return kIll.deathCount; }

    public void AddDeathCount(int index) { kIll.deathCount[index]++; }
    public void AdDKillCount(int index) { kIll.killCount[index]++; }


    public List<bool> GetPlayersLife() { return playersLife; }
    public List<bool> GetEnemyLife() { return enemyLife; }

    public List<GameObject> GetchracterALL() 
    {
        List<GameObject> list = new List<GameObject>();

        list.Add(player);

        for (int i=0;i< players.Count;i++) 
        {
            list.Add(players[i].gameObject);
        }
        for (int i=0;i< enemys.Count;i++) 
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
        if(player==null) player = GameObject.FindGameObjectWithTag("Player");

        ScanAILife();
        Debug();
    }
    public int GetID()
    {
        int number = IDNumber;
        IDNumber++;
        return number;
    }

    public Vector3 PlayerFlagPosition() { return flagObject[0].transform.position; }

    public void DataSave()
    {
        KillData.InGameDataSave(kIll);
    }




    public List<GameObject> GetRelativeEnemy(bool isEnemyTeam)
    {
        int count;

        if (isEnemyTeam) count = players.Count;
        else count = enemys.Count;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            if (isEnemyTeam)
            {
                if (!players[i].GetStatus().GetISLife()) continue;
                list.Add(players[i].gameObject);
            }
            else
            {
                if (!enemys[i].GetStatus().GetISLife()) continue;
                list.Add(enemys[i].gameObject);
            }
        }

        if (isEnemyTeam && player != null && !PlayerManager.IsPlayerDead()) list.Add(player);

        if (GameModes.mode == PublicEnum.GameMode.deathmatch)
        {
            list.Clear();
            for (int i = 0; i < enemys.Count; i++)
            {
                if (!enemys[i].GetStatus().GetISLife()) continue;
                list.Add(enemys[i].gameObject);
            }
            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].GetStatus().GetISLife()) continue;
                list.Add(players[i].gameObject);
            }
            list.Add(player);
        }

        return list;

    }

    private void ScanAILife()
    {
        playersLife.Clear();
        players.Capacity = players.Capacity;
        for (int i = 0; i < players.Count; i++)
        {
            playersLife.Add(AICharacterUtility.GetCharacterAI(i).GetISLife());

        }

        enemyLife.Clear();
        enemyLife.Capacity = enemys.Capacity;
        for (int i = 0; i < enemys.Count; i++)
        {
            enemyLife.Add(AICharacterUtility.GetCharacterAI(i + 4).GetISLife());

        }


    }

    public void SetFlagObject(GameObject flagObject, int number)
    {
        this.flagObject[number] = flagObject;
    }

    public void CreateAI()
    {
        GanObject.LoodGameObject();

        SoundManager.Initialize();

        kIll = KillData.InGameDataLoad();

        kIll.killCount.Clear();
        kIll.deathCount.Clear();
        kIll.killCount.Add(0);
        kIll.deathCount.Add(0);





        for (int i = 0; i < flagObject.Length; i++)
        {

            Vector3 vec = flagObject[(i + 1) % 2].transform.position - flagObject[i].transform.position;

            float angle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

            for (int j = 0; j < AI_NUMBER; j++)
            {
                if (i == 0 && j == 0) continue;
                GameObject ai = GameObject.Instantiate(origenAI);

                float createAngle = angle + (15 * j) - 30;
                RaandomGan(ai);


                ai.transform.eulerAngles = new Vector3(0, createAngle, 0);

                ai.transform.position = flagObject[i].transform.position + new Vector3(Mathf.Sin(createAngle * Mathf.Deg2Rad), 0, Mathf.Cos(createAngle * Mathf.Deg2Rad));
                if (GameModes.mode == PublicEnum.GameMode.deathmatch)
                {

                    int mapMax = CreateMapManager.GetMAPMAXSIZE() * 5 - 1;

                    Vector2 mapReta = CreateMapManager.GetMapRate();


                    createAngle = 36 * ((i * 5) + j);
                    ai.transform.eulerAngles = new Vector3(0, createAngle + 180, 0);

                    ai.transform.position = new Vector3(Mathf.Sin(createAngle) * mapMax, 0, Mathf.Cos(createAngle) * mapMax);

                    ai.transform.position += new Vector3((mapReta.x * 10 - 10) / 2, 0, (mapReta.y*10 - 10) / 2);



                }
                ai.transform.name += ((i * 5) + j).ToString();
                AI Ai = ai.GetComponent<AI>();




                Ai.SetEnemyFlag(flagObject[(i + 1) % 2]);
                Ai.SetFlag(flagObject[i]);

                kIll.killCount.Add(0);
                kIll.deathCount.Add(0);


                if (i <= 0)
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[0];

                    Ai.SetPlayerFaction(() => false);
                    players.Add(Ai);

                }
                else
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[1];

                    Ai.SetPlayerFaction(() => true);
                    enemys.Add(Ai);
                }

            }

        }



    }


    private void Debug()
    {
        if (Input.GetKey(KeyCode.Alpha0)) players[0].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha1)) players[1].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha2)) players[2].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha3)) players[3].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha4)) enemys[0].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha5)) enemys[1].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha6)) enemys[2].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha7)) enemys[3].gameObject.GetComponent<CharacterInsterface>().HitAction();
        if (Input.GetKey(KeyCode.Alpha8)) enemys[4].gameObject.GetComponent<CharacterInsterface>().HitAction();
    }

    private void RaandomGan(GameObject ai)
    {
        GanObject.ConstancyGanType type = (ConstancyGanType)Random.Range(0, (int)GanObject.ConstancyGanType.Max - 1);
        Animator animator = ai.GetComponent<Animator>();
        int randomRenge = 0;

        GameObject gan = GameObject.Instantiate(GanObject.constancyGun.objects[0]);

        AI aI = ai.GetComponent<AI>();

        aI.SetGanObject(gan);
        aI.Initialization();

        aI.GetIShot().SetGanType(type);
        aI.SetBullet(GanObject.GanBulletCount[(int)type]);

        switch (ConstancyGanType.SL_8)
        {
            case ConstancyGanType.SL_8:
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("MoveSpped", 1);
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("ShotSpped", 2);
                randomRenge = 5;
                break;
            case ConstancyGanType.Classic:
                animator.runtimeAnimatorController = HandGanType;
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("MoveSpped", 1.1f);
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("ShotSpped", 3);
                randomRenge = 7;

                break;
            case ConstancyGanType.Stechkin:
                animator.runtimeAnimatorController = HandGanType;
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("MoveSpped", 1);
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("ShotSpped", 0.5f);
                randomRenge = 3;

                break;
            case ConstancyGanType.FAR_EYE:
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("MoveSpped", 0.5f);
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("ShotSpped", 0.3f);
                randomRenge = 0;
                break;
            case ConstancyGanType.EyeOfHorus:
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("MoveSpped", 0.8f);
                ai.GetComponent<AI>().GetStatus().SetAnimatorFloat("ShotSpped", 0.8f);
                randomRenge = 10;
                break;
        }


        gan.transform.parent = ai.transform;

        WeaponEquipment weapon = gan.AddComponent<WeaponEquipment>();


        weapon.SetLefthand(aI.GetLeftHand());
        weapon.SetRighthand(aI.GetRightHand());

        aI.SetRandomRenge(randomRenge);


    }


}


public class KIllCount 
{

    public List<int> deathCount = new List<int>(AIManager.AI_NUMBER * 2) { 0 };
    public List<int> killCount = new List<int>(AIManager.AI_NUMBER * 2) { 0 };
}