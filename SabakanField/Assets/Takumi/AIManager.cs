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
    [SerializeField] GameObject origenAI;

    private readonly int AI_NUMBER = 5;

    public void FixedUpdate()
    {
        ScanAILife();

    }

    private void ScanAILife() 
    {
        playersLife.Clear();
        for(int i = 0; i < players.Count; i++) 
        {
            playersLife[i] = players[i].GetISLife();

        }

        enemyLife.Clear();
        for(int i = 0; i < enemys.Count; i++) 
        {
            enemyLife[i] = enemys[i].GetISLife();

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

                ai.transform.position = flagObject[i].transform.position + new Vector3(Mathf.Sin(createAngle*Mathf.Deg2Rad),0,Mathf.Cos(createAngle * Mathf.Deg2Rad));

                ai.transform.eulerAngles = new Vector3(0, createAngle, 0);

                AIMove aIMove = ai.GetComponent<AIMove>();

                aIMove.SetFlagAngle(flagObject[(i + 1) % 2]);



                if (i < 0) 
                {

                    aIMove.SetPlayerFaction(() => false);
                    players.Add(aIMove); 
                
                }
                else 
                {
                    aIMove.SetPlayerFaction(() => true);
                    enemys.Add(aIMove); 
                }

            }

        }



    }




}
