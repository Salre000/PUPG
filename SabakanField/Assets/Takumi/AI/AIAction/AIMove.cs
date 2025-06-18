using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AIMove
{
    private GameObject thisGameObject;

    public void SetThisGameObject(GameObject thisGameObject)
    { this.thisGameObject = thisGameObject; }

    private int ID;
    public void SetID(int getID) { ID = getID; }

    private Vector3[] RAYCAST_OFFSET =
        {
         new Vector3(0, 1f, 0),
         new Vector3(0, 0.5f, 0),
         new Vector3(0, 0.5f, 0),
         new Vector3(0, 0.5f, 0),
    };

    private readonly float ChengeAngleRange = 10;

    private readonly float AngleRange = 5;


    private bool rotateFlag = false;

    private float daleyTime = 0;


    private Vector3 gameStartPosition = Vector3.zero;

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

    public enum NowMode
    {
        Wandering,
        Shot,
        Back,
        Chase,
        ChageAngle

    }
    [SerializeField] public NowMode nowMode = NowMode.Wandering;

    [SerializeField] public NowMode nextMode = NowMode.Wandering;

    [SerializeField] float nextMoveAngle = 0;

    [SerializeField] GameObject targetObject;

    public float GetNextAngle() { return nextMoveAngle; }

    private readonly float _EPSILON = 5.0f;


    public void Start()
    {
        Vector3 position = thisGameObject.transform.position;
        position.y = 0;
        thisGameObject.transform.position = position;
        gameStartPosition = thisGameObject.transform.position;


    }



    public void FixedUpdate()
    {
        if (!AICharacterUtility.GetCharacterAI(ID).GetISLife()) return;

        ChackDash();

        SearchEnemy();

        switch (nowMode)
        {
            case NowMode.Wandering:
                Wandering();
                break;
            case NowMode.Shot:
                Shoter();
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
        Debug.Log("射撃終了");


        if (SearchHitEnemy()) 
        {

            AICharacterUtility.GetCharacterAI(ID).SetAnimatorTrigger("Shot");


        }
        else 
        {
        nowMode = NowMode.Wandering;
        AICharacterUtility.SetShotFlag(ID, false);
        }




    }

    public void ReStart()
    {
        nowMode = NowMode.Wandering;
        AICharacterUtility.SetShotFlag(ID, false);

        ResetAnimation();
    }
    private void ChackDash()
    {


    }
    private bool SearchHitEnemy() 
    {
        List<GameObject> targets = AICharacterFunction.TargetEnemysInAngle(thisGameObject, AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return false;

        Vector3 startPos = thisGameObject.transform.position + RAYCAST_OFFSET[0] + thisGameObject.transform.forward;

        GameObject hitObject = AICharacterFunction.TargetGetAngle(targets, ID, thisGameObject, startPos);

        if (hitObject == null) return false;

        Debug.Log("内積" + Vector3.Dot(thisGameObject.transform.forward, (hitObject.transform.position - thisGameObject.transform.position).normalized) * Mathf.Rad2Deg);

        if (Vector3.Dot(thisGameObject.transform.forward, (hitObject.transform.position - thisGameObject.transform.position).normalized) * Mathf.Rad2Deg > 50) return false;
        targetObject = hitObject;

        return true;
    }
    private void SearchEnemy()
    {
        if (shotingFlag) return;

        List<GameObject> targets = AICharacterFunction.TargetEnemysInAngle(thisGameObject, AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return;

        Vector3 startPos = thisGameObject.transform.position + RAYCAST_OFFSET[0] + thisGameObject.transform.forward;

        GameObject hitObject = AICharacterFunction.TargetGetAngle(targets, ID, thisGameObject, startPos);

        if (hitObject == null) return;

        Debug.Log("内積" + Vector3.Dot(thisGameObject.transform.forward, (hitObject.transform.position - thisGameObject.transform.position).normalized) * Mathf.Rad2Deg);

        //視野角
        //if (Vector3.Dot(thisGameObject.transform.forward,(hitObject.transform.position - thisGameObject.transform.position).normalized)*Mathf.Rad2Deg > 50) return;


        nowMode = NowMode.Shot;

        targetObject = hitObject;
        Debug.DrawLine(thisGameObject.transform.position, targetObject.transform.position, Color.yellow,1);

    }

    [SerializeField] public bool shotingFlag = false;
    public void SetShotFlag(bool flag) { shotingFlag = flag; }

    private void Wandering()
    {

        if (rotateFlag) daleyTime += Time.deltaTime;

        Vector3 startPos = thisGameObject.transform.position + thisGameObject.transform.forward / 10;

        for (int i = 0; i < RAYCAST_OFFSET.Length; i++)
        {

            RaycastHit hit;

            if (Physics.Raycast(startPos + RAYCAST_OFFSET[i], thisGameObject.transform.forward, out hit))
            {

                if (Vector3.Distance(thisGameObject.transform.position, hit.point) > AngleRange) continue;



                if (rotateFlag) return;

                rotateFlag = true;
                Debug.Log("回転かいし");
                if (ChengeAngle(startPos, i, hit.transform.gameObject)) return;
            }


        }


        if (daleyTime < 0.2f) return;

        ResetAnimation();


    }

    private void Shoter()
    {
        if (shotingFlag) return;

        Vector3 vec=targetObject.transform.position-thisGameObject.transform.position;

        float angle = Mathf.Atan2(vec.x, vec.z);

        thisGameObject.transform.eulerAngles = new Vector3(0,angle*Mathf.Rad2Deg,0);

        AICharacterUtility.GetCharacterAI(ID).SetAnimatorTrigger("Shot");

        Debug.Log("射撃" + ID);
        AICharacterUtility.SetShotFlag(ID, true);

    }

    private void ChangeAngle()
    {

        float angle = thisGameObject.transform.eulerAngles.y;
        angle += 360.0f;
        angle %= 360.0f;

        if (angle > nextMoveAngle + _EPSILON || angle < nextMoveAngle - _EPSILON) return;
        ResetAnimation();

        nowMode = nextMode;
    }

    public float GetTargetAngle() 
    {
        if (targetObject == null)  { Debug.Log("対象なし");  return -1; }

        Vector3 vec= targetObject.transform.position-thisGameObject.transform.position;

        float angle=Mathf.Atan2(vec.x,vec.z)*Mathf.Rad2Deg;

        return angle;


    }


    private void ResetAnimation()
    {
        AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Left", false);
        AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Right", false);
        AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Back", false);
        rotateFlag = false;
        daleyTime = 0;
    }



    private bool ChengeAngle(Vector3 startPos, int heightNumber, GameObject tragetObject)
    {
        //���݂̊p�x
        float nowAngle = Mathf.Atan2(thisGameObject.transform.forward.x, thisGameObject.transform.forward.z) * Mathf.Rad2Deg;

        ChengeAngleType angleType = ChengeAngleType.None;

        //�ύX�����p�x�̗�
        float angleVec = 0;

        Vector3 rayAngle = Vector3.zero;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //�������̕����x�N�g��
            rayAngle.x = Mathf.Sin((nowAngle + angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle + angleVec) * Mathf.Deg2Rad);

            //�������̃��C
            if (Physics.Raycast(startPos + RAYCAST_OFFSET[heightNumber], rayAngle, out hit))
            {
                if (Vector3.Distance(thisGameObject.transform.position, hit.point) > ChengeAngleRange)
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
                if (Vector3.Distance(thisGameObject.transform.position, hit.point) > ChengeAngleRange)
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
            //�ύX������������܂�����
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
                AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Left", true);
                break;
            case ChengeAngleType.Right:
                AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Right", true);
                break;
        }


    }

    private GameObject hitObject;

    private float countTime = 5;


    Vector3 endPosition = Vector3.zero;

    public void HitObject(Collision collision)
    {

        if (shotingFlag && nowMode != NowMode.Shot)
        {
            return;
        }

        if (collision.transform.tag == "Floor") return;

        endPosition = thisGameObject.transform.position + thisGameObject.transform.forward;

        hitObject = collision.gameObject;

        countTime = 5;

        nowMode = NowMode.Wandering;
        ResetAnimation();

        thisGameObject.transform.LookAt(hitObject.transform);

        thisGameObject.transform.eulerAngles = new Vector3(0, thisGameObject.transform.eulerAngles.y + 180f, 0);

    }

    public void Resurrect(System.Action action)
    {
        ResetAnimation();

        Vector3 vec = playerFlag.transform.position - gameStartPosition;
        float angle = Mathf.Atan2(vec.x, vec.z);
        AICharacterUtility.GetCharacterAI(ID).SetAnimatorFloat("DeathSpped", 0);
        Vector3 pos = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 5 + playerFlag.transform.position;

        int mapMax = CreateMapManager.GetMAPMAXSIZE() * 5 - 1;

        Vector2 mapReta = CreateMapManager.GetMapRate();

        if (GameModes.mode == PublicEnum.GameMode.deathmatch)
        {

            pos = new Vector3((mapReta.x * 10 - 10) / 2, 0, (mapReta.y * 10 - 10) / 2);

            angle = UnityEngine.Random.Range(0, 360);

            thisGameObject.transform.eulerAngles = new Vector3(0, angle + 180, 0);

            angle *= Mathf.Deg2Rad;

            pos += new Vector3(Mathf.Sin(angle) * mapMax, 0, Mathf.Cos(angle) * mapMax);




        }

        RespawnManager.Instance.DelayRespawn(thisGameObject, pos, UnityEngine.Random.Range(6, 15), () =>
        {

            AICharacterUtility.GetCharacterAI(ID).SetAnimatorFloat("DeathSpped", 1);
            AICharacterUtility.GetCharacterAI(ID).SetISLife(true);
            action();
        });


    }

    public void GroundSet()
    {
        Vector3 position = thisGameObject.transform.position;
        position.y = 0;
    }
}
