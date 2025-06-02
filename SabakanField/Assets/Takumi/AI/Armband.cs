using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armband : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;

    public void SetWeaponPosition()
    {



    }

    public void FixedUpdate()
    {
        this.transform.position = (start.transform.position-end.transform.position)/2+end.transform.position;

        transform.LookAt(end.transform);

    }
}
