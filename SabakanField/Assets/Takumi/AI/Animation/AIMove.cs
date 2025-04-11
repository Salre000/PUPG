using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private readonly Vector3[] RAYCAST_OFFSET = 
        { 
         new Vector3(0, 1f, 0),
         new Vector3(0, 5f, 0),
    };

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

        Vector3 startPos = this.transform.position + this.transform.forward;

        for (int i = 0; i < RAYCAST_OFFSET.Length; i++)
        {
            Debug.DrawRay(startPos + RAYCAST_OFFSET[i], this.transform.forward * 5);

            RaycastHit hit;

            if (Physics.Raycast(startPos + RAYCAST_OFFSET[i], this.transform.forward, out hit)) // もしRayを投射して何らかのコライダーに衝突したら
            {
                if (Vector3.Distance(this.transform.position, hit.point) > 5) continue;

                if (rotateFlag) return;
                rotateFlag=true;
                if (ChengeAngle(startPos,i)) return;
            }


        }
        Debug.Log(daleyTime);
        if (daleyTime < 0.2f) return;

        ResetAnimation();

    }

    //レイに何も当たらなかったときの処理
    private void ResetAnimation()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        rotateFlag = false;
        daleyTime = 0;
    }

    private bool ChengeAngle(Vector3 startPos, int heightNumber)
    {
        //現在の角度
        float nowAngle = Mathf.Atan2(this.transform.forward.x, this.transform.forward.z) * Mathf.Rad2Deg;

        ChengeAngleType angleType = ChengeAngleType.None;

        //変更した角度の量
        float angleVec = 0;

        Vector3 rayAngle=Vector3.zero;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //左方向の方向ベクトル
            rayAngle.x=Mathf.Sin((nowAngle+angleVec)*Mathf.Deg2Rad);
            rayAngle.z=Mathf.Cos((nowAngle+angleVec)*Mathf.Deg2Rad);

            //左方向のレイ
            if (Physics.Raycast(startPos + RAYCAST_OFFSET[heightNumber], rayAngle, out hit))
            {
                if (Vector3.Distance(this.transform.position, hit.point) > 5)
                {

                    angleType = ChengeAngleType.Left;

                }
            }

            //右方向の方向ベクトル
            rayAngle.x = Mathf.Sin((nowAngle - angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle - angleVec) * Mathf.Deg2Rad);


            //右方向のレイ
            if (Physics.Raycast(startPos + RAYCAST_OFFSET[heightNumber], rayAngle, out hit)) 
            {
                if (Vector3.Distance(this.transform.position, hit.point) > 5) 
                {

                    angleType=ChengeAngleType.Right;
                }
            }



            //変更する方向が決まったら
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
