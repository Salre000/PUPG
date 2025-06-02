using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAngle : MonoBehaviour
{
    private void FixedUpdate()
    {
        this.transform.Rotate(new Vector3(0,1,0));
    }
}
