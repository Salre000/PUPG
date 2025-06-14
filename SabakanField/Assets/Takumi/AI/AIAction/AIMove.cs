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

    private readonly Vector3[] RAYCAST_OFFSET =
        {
         new Vector3(0, 1f, 0),
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
    [SerializeField]public NowMode nowMode = NowMode.Wandering;

    [SerializeField]public  NowMode nextMode = NowMode.Wandering;

    [SerializeField] float nextMoveAngle = 0;

    public float GetNextAngle() {  return nextMoveAngle; }

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
        Debug.Log("射撃終了");
        nowMode = NowMode.Wandering;
        AICharacterUtility.SetShotFlag(ID, false);

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

    private void SearchEnemy()
    {
        if (shotingFlag) return;

        List<GameObject> targets =AICharacterFunction.TargetEnemysInAngle(thisGameObject,AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return;

        Vector3 startPos = thisGameObject.transform.position + RAYCAST_OFFSET[0] + thisGameObject.transform.forward;

        GameObject hitObject= AICharacterFunction.TargetGetAngle(targets,ID,thisGameObject, startPos);

        if (hitObject == null) return;

        thisGameObject.GetComponent<AI>();

        AICharacterUtility.SetShotFlag(ID, true);

        Vector3 tragetDir = hitObject.transform.position - thisGameObject.transform.position;

        nextMoveAngle = Mathf.Atan2(tragetDir.x, tragetDir.z) * Mathf.Rad2Deg;

        nextMoveAngle += 360.0f;
        nextMoveAngle %= 360.0f;

        Vector3 Cross = Vector3.Cross(thisGameObject.transform.forward, tragetDir);

        if (Cross.y < 0)
            AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Left", true);
        else AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Right", true);
        nowMode = NowMode.ChageAngle;
        nextMode = NowMode.Shot;


    }

    [SerializeField] public bool shotingFlag = false;
    public void SetShotFlag(bool flag) {  shotingFlag = flag; }

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



                //��]���������牽�����Ȃ�
                if (rotateFlag) return;

                rotateFlag = true;
                if (ChengeAngle(startPos, i, hit.transform.gameObject)) return;
            }


        }
        if (daleyTime < 0.2f) return;

        ResetAnimation();


    }

    //������AI�̊p�x�Ɏw��������������K�v������
    private void ChangeAngle()
    {

        float angle = thisGameObject.transform.eulerAngles.y;
        angle += 360.0f;
        angle %= 360.0f;

        if (angle > nextMoveAngle + _EPSILON || angle < nextMoveAngle - _EPSILON) return;
        ResetAnimation();

        nowMode = nextMode;

        if (nowMode == NowMode.Shot)
            AICharacterUtility.GetCharacterAI(ID).SetAnimatorTrigger("Shot");


    }

    //���C�ɉ���������Ȃ������Ƃ��̏���
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

    private float countTime = 3;

    private void Back()
    {
        countTime-=Time.deltaTime;
        if (Vector3.Distance(hitObject.transform.position, thisGameObject.transform.position) > countTime) return;

        ResetAnimation();


        nextMode = NowMode.Wandering;

        nowMode = NowMode.Wandering;



        hitObject = null;
    }

    Vector3 endPosition = Vector3.zero;

    public void HitObject(Collision collision)
    {
        if (nowMode == NowMode.Back) return;

        if (shotingFlag && nowMode != NowMode.Shot) 
        {
            //AICharacterUtility.SetShotFlag(ID, false);
            return;
        }

        if (collision.transform.tag == "Floor") return;

        endPosition = thisGameObject.transform.position + thisGameObject.transform.forward;

        hitObject = collision.gameObject;

        countTime = 3;

        AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Back", true);
        nowMode = NowMode.Back;

        //thisGameObject.transform.LookAt(hitObject.transform);

        thisGameObject.transform.eulerAngles = new Vector3(0, thisGameObject.transform.eulerAngles.y, 0);

    }

    public void Resurrect(System.Action action)
    {
        ResetAnimation();

        Vector3 vec = playerFlag.transform.position - gameStartPosition;
        float angle = Mathf.Atan2(vec.x, vec.z);
        AICharacterUtility.GetCharacterAI(ID).SetAnimatorFloat("DeathSpped", 0);
        Vector3 pos = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 5 + playerFlag.transform.position;

        int mapMax = CreateMapManager.GetMAPMAXSIZE() * 5-1;

        Vector2 mapReta = CreateMapManager.GetMapRate();

        if (GameModes.mode == PublicEnum.GameMode.deathmatch) 
        {

            pos = new Vector3((mapReta.x*10-10)/2, 0, (mapReta.y * 10 - 10) / 2);

            angle = UnityEngine.Random.Range(0, 360);

            thisGameObject.transform.eulerAngles = new Vector3(0, angle+180, 0);

            angle *= Mathf.Deg2Rad;

            pos += new Vector3(Mathf.Sin(angle) * mapMax, 0, Mathf.Cos(angle)* mapMax);




        }
        
        RespawnManager.Instance.DelayRespawn(thisGameObject, pos,UnityEngine.Random.Range(6,15), () => 
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
