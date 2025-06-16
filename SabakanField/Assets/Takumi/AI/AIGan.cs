using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGan : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BulletPosition;

    public void Shot() 
    {
        GameObject bullet= GameObject.Instantiate(Bullet);

        bullet.transform.position=BulletPosition.transform.position;

        bullet.transform.eulerAngles = BulletPosition.transform.eulerAngles;

        bullet.GetComponent<Rigidbody>().velocity = BulletPosition.transform.forward * 1000;


    }
}
