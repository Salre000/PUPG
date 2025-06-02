using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReChage : MonoBehaviour
{
    GameObject player;
    [SerializeField] float renge = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        if (Vector3.Distance(player.transform.position,this.transform.position)< renge)
        {
            BulletManager.SetMAXBulletMagazine();
            //Debug.DrawLine(this.transform.position, player.transform.position, Color.green);

        }
        else 
        {
            //Debug.DrawLine(this.transform.position, player.transform.position, Color.red);

        }
    }
}
