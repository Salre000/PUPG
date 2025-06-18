using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{

    [SerializeField]private float _damage=-1;

    public void SetDamage(float damage) {  _damage = damage; }
    public float GetDamage() { return _damage; }    

}
