using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGan : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BulletPosition;
    [SerializeField, Header("Ç±ÇÃèeÇÃçUåÇóÕ")] float bulletDamage = -1;
    /*[SerializeField, Header("Ç±ÇÃèeÇÃë¨ìx")] */float bulletSpeed = 100;


    public void Start()
    {
        AI ai=GetComponentInParent<AI>();

        ai.SetIGan(this);
    }

    public void Shot() 
    {


        GameObject bullet= GameObject.Instantiate(Bullet);

        bullet.transform.position=BulletPosition.transform.position;
        bullet.transform.eulerAngles = new Vector3(0,BulletPosition.transform.eulerAngles.y,0);

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

        bullet.GetComponent<BulletDamage>().SetDamage(bulletDamage);

    }
}
