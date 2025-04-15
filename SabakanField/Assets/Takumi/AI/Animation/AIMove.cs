using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour,BulletMove
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

    private bool isLife = true;

    private Vector3 gameStartPosition = Vector3.zero;

    public bool GetISLife() {  return isLife; } 
    GameObject flag ;
    public void SetFlagAngle(GameObject flag) { this.flag = flag; }
    enum ChengeAngleType
    {
        Left,
        Right,
        None
    }

    enum NowMode 
    {
        Wandering,
        Shot,
        Back,
        Chase,
        ChageAngle

    }
    [SerializeField]NowMode nowMode = NowMode.Wandering;

    NowMode nextMode= NowMode.Wandering;

    [SerializeField]float nextMoveAngle = 0;

    private readonly float _EPSILON = 5.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameStartPosition=this.transform.position;
    }

    public void HitAction()
    {
        Debug.Log(this.gameObject.name + "レイに当たった");

    }


    private void FixedUpdate()
    {

        Debug.DrawRay(this.transform.position + RAYCAST_OFFSET[0],(flag.transform.position-this.transform.position)*5);


        switch (nowMode)
        {
            case NowMode.Wandering:
                Wandering();
                break;
            case NowMode.Shot:
                break;
            case NowMode.Back:
                Back();
                break;
            case NowMode.Chase:
                break;
            case NowMode.ChageAngle:
                ChangeAngle();
                break;
        }
    }
    public void EndShot() { nowMode = NowMode.Wandering; }
    private void Wandering() 
    {

        if (rotateFlag) daleyTime += Time.deltaTime;

        Vector3 startPos = this.transform.position + this.transform.forward / 10;

        for (int i = 0; i < RAYCAST_OFFSET.Length; i++)
        {
            Debug.DrawRay(startPos + RAYCAST_OFFSET[i], this.transform.forward * AngleRange);

            RaycastHit hit;

            if (Physics.Raycast(startPos + RAYCAST_OFFSET[i], this.transform.forward, out hit))
            {
                BulletMove bulletMove = hit.transform.gameObject.GetComponentInParent<BulletMove>();
                if (bulletMove != null && i < 2)
                {
                    animator.SetTrigger("Shot");
                    nowMode = NowMode.Shot;

                    return;
                }

                if (Vector3.Distance(this.transform.position, hit.point) > AngleRange) continue;


                BulletManager.RayHitTest(startPos + RAYCAST_OFFSET[i], this.transform.forward);

                //回転中だったら何もしない
                if (rotateFlag) return;

                rotateFlag = true;
                if (ChengeAngle(startPos, i, hit.transform.gameObject)) return;
            }


        }
        if (daleyTime < 0.2f) return;

        ResetAnimation();


    }

    //ここでAIの角度に指向性を持たせる必要がある
    private void ChangeAngle() 
    {
        float angle=this.transform.eulerAngles.y;
        angle += 360.0f;
        angle %= 360.0f;

        if (angle > nextMoveAngle + _EPSILON || angle < nextMoveAngle - _EPSILON) return;
        ResetAnimation();

        nowMode = nextMode;

    }

    //レイに何も当たらなかったときの処理
    private void ResetAnimation()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Back", false);
        rotateFlag = false;
        daleyTime = 0;
    }



    private bool ChengeAngle(Vector3 startPos, int heightNumber,GameObject tragetObject)
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

            //右方向の方向ベクトル
            rayAngle.x = Mathf.Sin((nowAngle - angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle - angleVec) * Mathf.Deg2Rad);


            //右方向のレイ
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



            if (angleVec >= 100) 
            {
                angleType = ChengeAngleType.Left;
                break;
            }
            //変更する方向が決まったら
            if (angleType != ChengeAngleType.None) break;
        }


        ChangeLRAnime(angleType);


        return true;

    }

    private void ChangeLRAnime(ChengeAngleType chenge) 
    {
        switch (chenge)
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


    }

    private GameObject hitObject;
    private void Back() 
    {
        if (Vector3.Distance(hitObject.transform.position, this.transform.position) > AngleRange) return;

        ResetAnimation();

        
        nextMode = NowMode.Wandering;

        nowMode = NowMode.ChageAngle;

        Vector3 vec=flag.transform.position-this.transform.position;
        //現在の角度
        float nowAngle = Mathf.Atan2(vec.x, vec.z)*Mathf.Rad2Deg;

        ChengeAngleType angleType = ChengeAngleType.None;

        //変更した角度の量
        float angleVec = 0;

        Vector3 rayAngle = Vector3.zero;

        nextMoveAngle = 0;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //左方向の方向ベクトル
            rayAngle.x = Mathf.Sin((nowAngle + angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle + angleVec) * Mathf.Deg2Rad);

            //左方向のレイ
            if (Physics.Raycast(this.transform.position + RAYCAST_OFFSET[0], rayAngle, out hit))
            {
                if (Vector3.Distance(this.transform.position, hit.point) > ChengeAngleRange)
                {
                    if (hitObject != hit.transform.gameObject)
                        nextMoveAngle = nowAngle + angleVec;
                }
            }

            //右方向の方向ベクトル
            rayAngle.x = Mathf.Sin((nowAngle - angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle - angleVec) * Mathf.Deg2Rad);


            //右方向のレイ
            if (Physics.Raycast(this.transform.position + RAYCAST_OFFSET[0], rayAngle, out hit))
            {
                if (Vector3.Distance(this.transform.position, hit.point) > ChengeAngleRange)
                {
                    if (hitObject != hit.transform.gameObject)
                        nextMoveAngle = nowAngle - angleVec;

                }
            }

            if (angleVec >= 100)
            {
                nextMoveAngle = 0;
                break;
            }
            //変更する方向が決まったら
            if (nextMoveAngle != 0) break;
        }


        ChangeLRAnime(ChengeAngleType.Left);

        nextMoveAngle += 360.0f;
        nextMoveAngle %= 360.0f;



        hitObject = null;
    }

    Vector3 endPosition = Vector3.zero;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor") return;

        endPosition = this.transform.position + this.transform.forward;

        hitObject = collision.gameObject;

        animator.SetBool("Back", true);
        nowMode = NowMode.Back;

        this.transform.LookAt(hitObject.transform);

        this.transform.eulerAngles=new Vector3( 0, this.transform.eulerAngles.y, 0);

    }

    public void Respawn() 
    {

        this.transform.position = gameStartPosition;


    }
}
