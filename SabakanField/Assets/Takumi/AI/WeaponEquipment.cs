using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;

    public void SetWeaponPosition() 
    {



    }

    public void FixedUpdate()
    {
        this.transform.position=rightHand.transform.position;

        transform.LookAt(leftHand.transform);

        this.transform.position -= this.transform.forward / 11;
    }

}
