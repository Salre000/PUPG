using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReChageAmo : MonoBehaviour
{
    private readonly float _RANGE = 8.0f;
    private GameObject player;
    Color color=Color.white;
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(this.transform.position,player.transform.position)<_RANGE)color = Color.green;
        else color = Color.red;

        Debug.DrawRay(this.transform.position+Vector3.up,player.transform.position-this.transform.position,color);

        //‚±‚±‚ÅƒvƒŒƒCƒ„[‚Ì’e‚ð•â[‚·‚é
    }


}
