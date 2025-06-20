using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGan : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject BulletPosition;
    [SerializeField, Header("Ç±ÇÃèeÇÃçUåÇóÕ")] float bulletDamage = -1;
    [SerializeField, Header("ÉVÉáÉbÉgÉKÉìÇ©Ç«Ç§Ç©")] bool shotGan = false;
    /*[SerializeField, Header("Ç±ÇÃèeÇÃë¨ìx")] */
    float bulletSpeed = 100;


    public void Start()
    {
        AI ai = GetComponentInParent<AI>();

        ai.SetIGan(this);
    }

    public void FixedUpdate()
    {


    }
    public void Shot(float angle)
    {


        GameObject bullet = GameObject.Instantiate(Bullet);

        bullet.transform.position = BulletPosition.transform.position;
        bullet.transform.eulerAngles = new Vector3(0, angle, 0);

        Debug.DrawRay(bullet.transform.position, bullet.transform.forward * 100, Color.red, 2);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        bullet.GetComponent<BulletDamage>().SetDamage(bulletDamage);

        if (!shotGan) return;
        Debug.Log("ÉVÉáÉbÉgÉKÉì");
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;
                bullet = GameObject.Instantiate(Bullet);

                bullet.transform.position = BulletPosition.transform.position;
                bullet.transform.eulerAngles = new Vector3(i*5, angle+j * 5, 0);
                Debug.DrawRay(bullet.transform.position, bullet.transform.forward * 100, Color.red, 2);

                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

                bullet.GetComponent<BulletDamage>().SetDamage(bulletDamage);
            }


        }

    }
}
