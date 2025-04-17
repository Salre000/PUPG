using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatus
{
    private bool isLife = true;

    private int ID = -1;

    private Animator animator;

    private CapsuleCollider collider;

    GameObject thisGameObject;

    //ÉQÅ[ÉÄäJénéûÇ…åƒÇ‘ä÷êî
    public void Start(GameObject gameObject) 
    {

        ID=AIUtility.GetID();
        thisGameObject = gameObject;

        collider = thisGameObject.GetComponent<CapsuleCollider>();
        animator = thisGameObject.GetComponent<Animator>();

    }

    public void HitAction()
    {
        if (!isLife) return;

        animator.SetTrigger("Death");
        isLife = false;
        AIUtility.AddDeathCount(ID);
        collider.enabled = false;

    }


    public void SetID(int id) { ID = id; }

    public int GetID() { return ID; }   
    public bool GetISLife() { return isLife; }

    public void SetISLife(bool flag) {  isLife = flag; }

    public Animator GetAnimator() { return animator; }

    public void SetAnimatorBool(string name,bool flag=true) { animator.SetBool(name,flag); }

    public void SetAnimatorTrigger(string name) {  animator.SetTrigger(name); } 


}
