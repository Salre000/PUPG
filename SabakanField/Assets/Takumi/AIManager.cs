using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    List<AIMove> players = new List<AIMove>();
    List<bool> playersLife = new List<bool>();
    List<AIMove> enemys = new List<AIMove>();
    List<bool> enemyLife = new List<bool>();

    GameObject []flagObject=new GameObject[2];
    [SerializeField] private Material []color=new Material[2];

    [SerializeField] GameObject origenAI;

    private readonly int AI_NUMBER = 5;

    public void Awake()
    {
        AIUtility.aIManager = this;
    }
    public void FixedUpdate()
    {
        ScanAILife();

    }

    public List<AIMove> GetRelativeEnemy(bool isPlayerTeam) 
    {
        if (isPlayerTeam) return enemys;
        else return players;

    }

    private void ScanAILife() 
    {
        playersLife.Clear();
        players.Capacity= players.Capacity;
        for(int i = 0; i < players.Count; i++) 
        {
            playersLife.Add(players[i].GetISLife());

        }

        enemyLife.Clear();
        enemyLife.Capacity=enemys.Capacity;
        for(int i = 0; i < enemys.Count; i++) 
        {
            enemyLife.Add(enemys[i].GetISLife());

        }


    }

    public void SetFlagObject(GameObject flagObject,int number) 
    {
        this.flagObject[number] = flagObject;
    }

    public void CreateAI() 
    {
        for(int i = 0; i < flagObject.Length; i++) 
        {
            Vector3 vec= flagObject[(i+1)%2].transform.position - flagObject[i].transform.position;

            float angle = Mathf.Atan2(vec.x, vec.z)*Mathf.Rad2Deg;

            for (int j = 0; j < AI_NUMBER; j++)
            {
                float createAngle = angle+(15*j)-30;
                GameObject ai=GameObject.Instantiate(origenAI);


                ai.transform.eulerAngles = new Vector3(0, createAngle, 0);

                ai.transform.position = flagObject[i].transform.position + new Vector3(Mathf.Sin(createAngle*Mathf.Deg2Rad),0,Mathf.Cos(createAngle * Mathf.Deg2Rad));

                AIMove aIMove = ai.GetComponent<AIMove>();

                aIMove.SetFlagAngle(flagObject[(i + 1) % 2]);
                aIMove.SetPlayerFlag(flagObject[i]);



                if (i < 0) 
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
