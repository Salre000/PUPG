using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField] GameObject leftHand;
    public void SetLefthand(GameObject left) { leftHand = left; }
    [SerializeField] GameObject rightHand;
    public void SetRighthand(GameObject right) { rightHand = right; }

    [SerializeField]public bool handgunFlag = false;

    Transform angleObject;
    float angloffSet = 0;
    private void Start()
    {
        angleObject = GetComponentInParent<AIIK>().handAnchorR;
        angloffSet = transform.eulerAngles.x;
    }

    public void FixedUpdate()
    {
        if (handgunFlag) 
        {
            this.transform.position = (rightHand.transform.position-leftHand.transform.position)/4+ leftHand.transform.position;

            this.transform.eulerAngles= angleObject.eulerAngles+new Vector3(angloffSet, 4.5f,90);

            return;
        }
        this.transform.position=rightHand.transform.position;

        transform.LookAt(leftHand.transform);
            this.transform.eulerAngles+=new Vector3(angloffSet, 3.5f,0);

        this.transform.position -= this.transform.forward / 11;
    }

}
