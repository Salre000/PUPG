using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGan : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BulletPosition;

    public void Start()
    {
        AI ai=GetComponentInParent<AI>();

        ai.SetIGan(this);
    }

    public void Shot(Vector3 angle) 
    {
        GameObject bullet= GameObject.Instantiate(Bullet);

        bullet.transform.position=BulletPosition.transform.position;

        bullet.transform.eulerAngles = angle;

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 75;


    }
}
