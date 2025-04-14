using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>();
    List<GameObject> enemys = new List<GameObject>();

    GameObject []flagObject=new GameObject[2];
    [SerializeField] GameObject origenAI;

    private readonly int AI_NUMBER = 5;

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

            }

        }



    }




}
