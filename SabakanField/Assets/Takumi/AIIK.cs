using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIK : MonoBehaviour
{
    public Transform handAnchorR = null;
    public Transform handAnchorL = null;

    private Animator animator;

    private float IKWeight = 0;

    public Vector3 vector3=Vector3.zero;

    // スタート時に呼ばれる
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // IK更新時に呼ばれる
    void OnAnimatorIK()
    {
        //右手
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, IKWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, IKWeight);
        animator.SetIKPosition(AvatarIKGoal.RightHand, handAnchorR.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, handAnchorR.rotation);
        

        // 左手
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, IKWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, handAnchorL.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, handAnchorL.rotation);
    }
    public void SetIK(int number) { IKWeight = number; }
    public void SetLeftPos(Vector3 pos) { handAnchorL.position = pos; }
    public void SetRightPos(Vector3 pos) { handAnchorR.position = pos; }
    public void SetLeftRotate(Vector3 pos) { handAnchorL.eulerAngles = pos+ vector3; }
    public void SetRightRotate(Vector3 pos) { handAnchorR.eulerAngles = pos + vector3; }

}
