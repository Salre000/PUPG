using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AILifeShow : MonoBehaviour
{
    [SerializeField] GameObject plyaers;
    [SerializeField] GameObject Enemys;
    private void FixedUpdate()
    {
        List<bool> plauers = AIUtility.GetPlayersLife();

        for (int i = 0; i < plauers.Count; i++) 
        {

            if (plyaers.transform.GetChild(i) == null) continue;

            if (plauers[i]) plyaers.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            else plyaers.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.gray;

        }

        List<bool> enemys = AIUtility.GetEnemysLife();

        for (int i = 0; i < enemys.Count; i++) 
        {

            if (Enemys.transform.GetChild(i) == null) continue;

            if (enemys[i]) Enemys.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            else Enemys.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.gray;

        }


    }
}
