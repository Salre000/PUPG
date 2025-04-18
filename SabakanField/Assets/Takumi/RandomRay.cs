using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomRay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float time = 0;
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time < 0.5f) return;
        time = 0;

        float angle = GetRandomAngle()*Mathf.Deg2Rad;

        Debug.DrawLine(this.transform.position,
            new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 30,Color.white,10000);

        
    }
    float GetRandomAngle() 
    {

        float angle = 0;
        for(int i = 0; i < 5; i++) 
        {

        angle-=UnityEngine.Random.Range(0, 5);
        angle+=UnityEngine.Random.Range(0, 5);
        }


        Debug.Log(angle);

        return angle* Mathf.Deg2Rad;


    }



}
