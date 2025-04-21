using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    //プレイヤーの味方のAIのリスト
    List<AI> players = new List<AI>();

    //プレイヤーの味方のAIの生存状況のリスト
    [SerializeField] List<bool> playersLife = new List<bool>();

    //敵のAIのリスト
    List<AI> enemys = new List<AI>();

    //敵のAI生存状況状況
    [SerializeField]List<bool> enemyLife = new List<bool>();

    //フラッグのオブジェクトの配列
    GameObject[] flagObject = new GameObject[2];

    //フラッグのオブジェクトの色を変更するマテリアルの配列
    [SerializeField] private Material[] color = new Material[2];

    //AIのオリジナルオブジェクト
    [SerializeField] GameObject origenAI;

    //1つの陣営のAIの数（プレイヤー側は-１）
    const int AI_NUMBER = 5;

    //プレイヤーの格納先
    GameObject player;

    private List<int> deathCount = new List<int>(AI_NUMBER * 2) { 0 };
    private List<int> killCount = new List<int>(AI_NUMBER * 2) { 0 };

    private int IDNumber = 0;

    public List<int> GetKillCount() { return killCount; }
    public List<int> GetDeathCount() { return deathCount; }

    public List<bool> GetPlayersLife() { return playersLife; }
    public List<bool> GetEnemyLife() { return enemyLife; }


    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public void FixedUpdate()
    {
        ScanAILife();

    }

    public void AddDeathCount(int index) { deathCount[index]++; }
    public void AdDKillCount(int index) { killCount[index]++; }

    public int GetID()
    {
        int number = IDNumber;
        IDNumber++;
        return number;
    }

    public Vector3 PlayerFlagPosition() { return flagObject[0].transform.position; }

    public void DataSave()
    {
        IDNumber = 0;
        AICharacterUtility.ClearCharacterAI();

        string[] kill = new string[killCount.Count];
        string[] death = new string[deathCount.Count];
        for (int i = 0; i < AI_NUMBER * 2; i++)
        {
            kill[i] = killCount[i].ToString();
            death[i] = deathCount[i].ToString();

        }
        DataSaveCSV.InGameDataSave(kill, death);
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

        if (isEnemyTeam && player != null&&!PlayerManager.IsPlayerDead()) list.Add(player);


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
            enemyLife.Add(AICharacterUtility.GetCharacterAI(i+4).GetISLife());

        }


    }

    public void SetFlagObject(GameObject flagObject, int number)
    {
        this.flagObject[number] = flagObject;
    }

    public void CreateAI()
    {

        killCount.Clear();
        deathCount.Clear();
        killCount.Add(0);
        deathCount.Add(0);





        for (int i = 0; i < flagObject.Length; i++)
        {

            Vector3 vec = flagObject[(i + 1) % 2].transform.position - flagObject[i].transform.position;

            float angle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;

            for (int j = 0; j < AI_NUMBER; j++)
            {
                if (i == 0 && j == 0) continue;

                float createAngle = angle + (15 * j) - 30;
                GameObject ai = GameObject.Instantiate(origenAI);


                ai.transform.eulerAngles = new Vector3(0, createAngle, 0);

                ai.transform.position = flagObject[i].transform.position + new Vector3(Mathf.Sin(createAngle * Mathf.Deg2Rad), 0, Mathf.Cos(createAngle * Mathf.Deg2Rad));

                ai.transform.name += ((i * 5) + j).ToString();

                AI aIMove = ai.GetComponent<AI>();

                aIMove.Initialization();

                aIMove.SetEnemyFlag(flagObject[(i + 1) % 2]);
                aIMove.SetFlag(flagObject[i]);

                killCount.Add(0);
                deathCount.Add(0);


                if (i <= 0)
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[0];

                    aIMove.SetPlayerFaction(() => false);
                    players.Add(aIMove);

                }
                else
                {
                    ai.transform.GetChild(0).GetComponent<MeshRenderer>().material = color[1];

                    aIMove.SetPlayerFaction(() => true);
                    enemys.Add(aIMove);
                }

            }

        }



    }




}
