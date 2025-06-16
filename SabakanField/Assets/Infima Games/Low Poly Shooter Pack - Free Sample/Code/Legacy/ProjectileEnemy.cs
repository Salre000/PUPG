using System;
using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;

public class ProjectileEnemy : MonoBehaviour
{

    [Range(5, 100)]
    [Tooltip("After how long time should the bullet prefab be destroyed?")]
    public float destroyAfter;
    [Tooltip("If enabled the bullet destroys on impact")]
    public bool destroyOnImpact = false;
    [Tooltip("Minimum time after impact that the bullet is destroyed")]
    public float minDestroyTime;
    [Tooltip("Maximum time after impact that the bullet is destroyed")]
    public float maxDestroyTime;

    [Header("Impact Effect Prefabs")]
    public Transform[] bloodImpactPrefabs;
    public Transform[] metalImpactPrefabs;
    public Transform[] dirtImpactPrefabs;
    public Transform[] concreteImpactPrefabs;
    public bool pierce = false;


    private Vector3 startPostion;
    private Vector3 lostPostion;

    private Vector3 dir;

    private void Start()
    {

        //Start destroy timer
        StartCoroutine(DestroyAfter());
        lostPostion = gameObject.transform.position;
        startPostion = gameObject.transform.position;

    }
    private void FixedUpdate()
    {
        dir = gameObject.transform.position - lostPostion;
        lostPostion = gameObject.transform.position;
    }

    //If the bullet collides with anything
    private void OnCollisionEnter(Collision collision)
    {
        //Ignore collisions with other projectiles.
        if (collision.gameObject.GetComponent<ProjectileEnemy>() != null)
            return;

        //“G‚É“–‚Ä‚ê‚é‚æ‚¤‚É‚·‚é
        BulletMoveFunction.RayHitTestMaterial(collision, gameObject, startPostion, dir);

        // //Ignore collision if bullet collides with "Player" tag
        // if (collision.gameObject.CompareTag("Player")) 
        // {
        // 	//Physics.IgnoreCollision (collision.collider);
        // 	Debug.LogWarning("Collides with player");
        // 	//Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
        //
        // 	//Ignore player character collision, otherwise this moves it, which is quite odd, and other weird stuff happens!
        // 	Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        //
        // 	//Return, otherwise we will destroy with this hit, which we don't want!
        // 	return;
        // }
        //
        //If destroy on impact is false, start 
        //coroutine with random destroy timer

        if (pierce)
        {
            GameObject copy = GameObject.Instantiate(this.gameObject);

            copy.transform.position = this.transform.position + transform.forward;

            copy.GetComponent<Rigidbody>().velocity = transform.forward * 1000;



        }


        if (!destroyOnImpact)
        {
            StartCoroutine(DestroyTimer());
        }
        //Otherwise, destroy bullet on impact
        else
        {
            Destroy(gameObject);
        }

        //If bullet collides with "Blood" tag
        if (collision.transform.tag == "Blood")
        {
            //Instantiate random impact prefab from array
            Instantiate(bloodImpactPrefabs[Random.Range
                (0, bloodImpactPrefabs.Length)], transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal));
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Metal" tag
        if (collision.transform.tag == "Metal")
        {
            //Instantiate random impact prefab from array
            Instantiate(metalImpactPrefabs[Random.Range
                (0, bloodImpactPrefabs.Length)], transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal));
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Dirt" tag
        if (collision.transform.tag == "Dirt")
        {
            //Instantiate random impact prefab from array
            Instantiate(dirtImpactPrefabs[Random.Range
                (0, bloodImpactPrefabs.Length)], transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal));
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Concrete" tag
        if (collision.transform.tag == "Concrete")
        {
            //Instantiate random impact prefab from array
            Instantiate(concreteImpactPrefabs[Random.Range
                (0, bloodImpactPrefabs.Length)], transform.position,
                Quaternion.LookRotation(collision.contacts[0].normal));
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "Target" tag
        if (collision.transform.tag == "Target")
        {
            //Toggle "isHit" on target object
            collision.transform.gameObject.GetComponent
                <TargetScript>().isHit = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "ExplosiveBarrel" tag
        if (collision.transform.tag == "ExplosiveBarrel")
        {
            //Toggle "explode" on explosive barrel object
            collision.transform.gameObject.GetComponent
                <ExplosiveBarrelScript>().explode = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        //If bullet collides with "GasTank" tag
        if (collision.transform.tag == "GasTank")
        {
            //Toggle "isHit" on gas tank object
            collision.transform.gameObject.GetComponent
                <GasTankScript>().isHit = true;
            //Destroy bullet object
            Destroy(gameObject);
        }

        Destroy(gameObject);

    }

    private IEnumerator DestroyTimer()
    {
        //Wait random time based on min and max values
        yield return new WaitForSeconds
            (Random.Range(minDestroyTime, maxDestroyTime));
        //Destroy bullet object
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfter()
    {
        //Wait for set amount of time
        yield return new WaitForSeconds(destroyAfter);
        //Destroy bullet object
        Destroy(gameObject);
    }
}