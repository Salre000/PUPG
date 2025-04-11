using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private readonly Vector3[] RAYCAST_OFFSET = 
        { 
         new Vector3(0, 1f, 0),
         new Vector3(0, 0.5f, 0),
    };

    private readonly float ChengeAngleRange = 10;

    private readonly float AngleRange = 5;

    private Animator animator;

    private bool rotateFlag=false;

    private float daleyTime = 0;



    enum ChengeAngleType
    {
        Left,
        Right,
        None
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if(rotateFlag) daleyTime+= Time.deltaTime;

        Vector3 startPos = this.transform.position + this.transform.forward/10;

        for (int i = 0; i < RAYCAST_OFFSET.Length; i++)
        {
            Debug.DrawRay(startPos + RAYCAST_OFFSET[i], this.transform.forward * AngleRange);

            RaycastHit hit;

            if (Physics.Raycast(startPos + RAYCAST_OFFSET[i], this.transform.forward, out hit)) 
            {
                if (Vector3.Distance(this.transform.position, hit.point) > AngleRange) continue;

                //��]���������牽�����Ȃ�
                if (rotateFlag) return;

                rotateFlag=true;
                if (ChengeAngle(startPos,i, hit.transform.gameObject)) return;
            }


        }
        Debug.Log(daleyTime);
        if (daleyTime < 0.2f) return;

        ResetAnimation();

    }

    //���C�ɉ���������Ȃ������Ƃ��̏���
    private void ResetAnimation()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        rotateFlag = false;
        daleyTime = 0;
    }

    private bool ChengeAngle(Vector3 startPos, int heightNumber,GameObject tragetObject)
    {
        //���݂̊p�x
        float nowAngle = Mathf.Atan2(this.transform.forward.x, this.transform.forward.z) * Mathf.Rad2Deg;

        ChengeAngleType angleType = ChengeAngleType.None;

        //�ύX�����p�x�̗�
        float angleVec = 0;

        Vector3 rayAngle=Vector3.zero;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //�������̕����x�N�g��
            rayAngle.x=Mathf.Sin((nowAngle+angleVec)*Mathf.Deg2Rad);
            rayAngle.z=Mathf.Cos((nowAngle+angleVec)*Mathf.Deg2Rad);

            //�������̃��C
            if (Physics.Raycast(startPos + RAYCAST_OFFSET[heightNumber], rayAngle, out hit))
            {
                if (Vector3.Distance(this.transform.position, hit.point) > ChengeAngleRange)
                {
                    if (tragetObject != hit.transform.gameObject)
                        angleType = ChengeAngleType.Right;
                }
            }
            else
            {
                angleType = ChengeAngleType.Right;

            }

            //�E�����̕����x�N�g��
            rayAngle.x = Mathf.Sin((nowAngle - angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle - angleVec) * Mathf.Deg2Rad);


            //�E�����̃��C
            if (Physics.Raycast(startPos + RAYCAST_OFFSET[heightNumber], rayAngle, out hit)) 
            {
                if (Vector3.Distance(this.transform.position, hit.point) > ChengeAngleRange) 
                {
                    if (tragetObject != hit.transform.gameObject)
                        angleType = ChengeAngleType.Left;
                }
            }
            else 
            {
                angleType = ChengeAngleType.Left;

            }




            //�ύX������������܂�����
            if (angleType != ChengeAngleType.None) break;
        }

        switch (angleType)
        {
            case ChengeAngleType.Left:
                animator.SetBool("Left", true);
                Debug.Log("Left");
                break;
            case ChengeAngleType.Right:
                animator.SetBool("Right", true);
                Debug.Log("Right");
                break;
        }

        return true;

    }


}
