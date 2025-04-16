using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AIMove : MonoBehaviour, CharacterInsterface
{
    private readonly Vector3[] RAYCAST_OFFSET =
        {
         new Vector3(0, 1f, 0),
         new Vector3(0, 0.5f, 0),
    };

    private readonly float ChengeAngleRange = 10;

    private readonly float AngleRange = 5;

    private Animator animator;

    private bool rotateFlag = false;

    private float daleyTime = 0;

    private bool isLife = true;

    private int ID = -1;
    public void SetID(int id) { ID= id; }

    private Vector3 gameStartPosition = Vector3.zero;

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return _PlayerFaction(); }

    public bool GetISLife() { return isLife; }
    GameObject flag;
    GameObject playerFlag;
    public void SetFlagAngle(GameObject flag) { this.flag = flag; }
    public void SetPlayerFlag(GameObject flag) { playerFlag = flag; }
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
    [SerializeField] NowMode nowMode = NowMode.Wandering;

    [SerializeField] NowMode nextMode = NowMode.Wandering;

    [SerializeField] float nextMoveAngle = 0;

    private readonly float _EPSILON = 5.0f;

    //自分のフラックとの距離がこれ以下だったら走って向かう
    private float DashRange = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        Vector3 position = this.transform.position;
        position.y = 0;
        this.transform.position = position;
        gameStartPosition = this.transform.position;

    }

    public void HitAction()
    {
        if (!isLife) return;

        Debug.Log(this.gameObject.name + "レイに当たった");
        animator.SetTrigger("Death");
        isLife = false;
        AIUtility.AddDeathCount(ID);

    }


    private void FixedUpdate()
    {
        if (!isLife) return;

        ChackDash();

        SearchEnemy();

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
    public void EndShot()
    {
        nowMode = NowMode.Wandering;
    }

    public void ReStart()
    {
        nowMode = NowMode.Wandering;
        shotingFlag = false;

        ResetAnimation();
    }

    bool dashFlag = false;
    private void ChackDash()
    {

        if (Vector3.Distance(playerFlag.transform.position, this.transform.position) < DashRange && !dashFlag)
        {
            //アニメーションをダッシュに変える
        }
        else if (dashFlag)
        {
            //アニメーションをゆっくりに変える
        }

    }

    private void SearchEnemy()
    {
        if (shotingFlag) return;

        List<GameObject> targets = TargetEnemysInAngle();

        if (targets.Count <= 0) return;

        TargetGetAngle(targets);

    }
    //このAIの視界内に敵はいるのかを判断する関数
    private List<GameObject> TargetEnemysInAngle()
    {

        List<GameObject> targetObjcets = AIUtility.GetRelativeEnemy(PlayerFaction());

        List<GameObject> targets = new List<GameObject>();


        for (int i = 0; i < targetObjcets.Count; i++)
        {
            Vector3 vec = targetObjcets[i].transform.position - this.transform.position;
            if (Vector3.Dot(this.transform.forward, vec) < 0.8) continue;



            targets.Add(targetObjcets[i].gameObject);

        }


        return targets;



    }

    //視界内にいる敵にrayが通るかを判断して角度を導く
    private GameObject TargetGetAngle(List<GameObject> targets)
    {
        GameObject hitObject = null;

        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit hit;
            Vector3 startPosition = this.transform.position + this.transform.forward + RAYCAST_OFFSET[0];

            Debug.DrawLine(startPosition, targets[i].transform.position, Color.yellow);


            Vector3 dir = targets[i].transform.position - this.transform.position;
            if (Physics.Raycast(startPosition, dir, out hit))
            {


                CharacterInsterface bullet = hit.transform.GetComponentInParent<CharacterInsterface>();


                if (bullet == null) continue;

                hitObject = hit.transform.gameObject;



            }


        }
        if (hitObject == null) return null;


        shotingFlag = true;
        Vector3 tragetDir = hitObject.transform.position - this.transform.position;

        nextMoveAngle = Mathf.Atan2(tragetDir.x, tragetDir.z) * Mathf.Rad2Deg;

        nextMoveAngle += 360.0f;
        nextMoveAngle %= 360.0f;

        Vector3 Cross = Vector3.Cross(this.transform.forward, tragetDir);

        if (Cross.y < 0) animator.SetBool("Left", true);
        else animator.SetBool("Right", true);
        nowMode = NowMode.ChageAngle;
        nextMode = NowMode.Shot;

        return hitObject;

    }

    [SerializeField] private bool shotingFlag = false;
    public void Shot()
    {

        Vector3 startPos = this.transform.position + this.transform.forward / 10 + RAYCAST_OFFSET[0];
        RaycastHit hit;

        List<GameObject> targets = TargetEnemysInAngle();
        shotingFlag = false;

        if (targets.Count <= 0) return;

        GameObject targetObject = TargetGetAngle(targets);
        shotingFlag = false;
        if (targetObject == null) return;

        Vector3 Vec = targetObject.transform.position - this.transform.position;

        Debug.DrawRay(startPos, Vec, Color.red, 1);

        if (Physics.Raycast(startPos, Vec, out hit))
        {
            GameObject ss = hit.transform.gameObject;
        }

        BulletMoveFunction.RayHitTest(startPos, Vec);

    }
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

                if (Vector3.Distance(this.transform.position, hit.point) > AngleRange) continue;



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

        float angle = this.transform.eulerAngles.y;
        angle += 360.0f;
        angle %= 360.0f;

        if (angle > nextMoveAngle + _EPSILON || angle < nextMoveAngle - _EPSILON) return;
        ResetAnimation();

        nowMode = nextMode;

        if (nowMode == NowMode.Shot) animator.SetTrigger("Shot");


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



    private bool ChengeAngle(Vector3 startPos, int heightNumber, GameObject tragetObject)
    {
        //現在の角度
        float nowAngle = Mathf.Atan2(this.transform.forward.x, this.transform.forward.z) * Mathf.Rad2Deg;

        ChengeAngleType angleType = ChengeAngleType.None;

        //変更した角度の量
        float angleVec = 0;

        Vector3 rayAngle = Vector3.zero;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //左方向の方向ベクトル
            rayAngle.x = Mathf.Sin((nowAngle + angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle + angleVec) * Mathf.Deg2Rad);

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

        Vector3 vec = flag.transform.position - this.transform.position;
        //現在の角度
        float nowAngle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;


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
        if (shotingFlag && nowMode != NowMode.Shot) 
        {
            shotingFlag = false;
            return;
        }

        if (collision.transform.tag == "Floor") return;

        endPosition = this.transform.position + this.transform.forward;

        hitObject = collision.gameObject;

        animator.SetBool("Back", true);
        nowMode = NowMode.Back;

        this.transform.LookAt(hitObject.transform);

        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);

    }

    public void Respawn()
    {
        ResetAnimation();

        Vector3 vec = playerFlag.transform.position - gameStartPosition;
        float angle = Mathf.Atan2(vec.x, vec.z);
        this.transform.position = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle))*5 + playerFlag.transform.position;
        isLife = true;

    }

    public void GroundSet()
    {
        Vector3 position = this.transform.position;
        position.y = 0;
    }
}
