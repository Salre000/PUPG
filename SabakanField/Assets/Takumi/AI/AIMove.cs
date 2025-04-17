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

    private System.Func<bool> _PlayerFaction;
    public void SetPlayerFaction(System.Func<bool> playerFaction) { _PlayerFaction = playerFaction; }

    public bool PlayerFaction() { return _PlayerFaction(); }

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

    private readonly float _EPSILON = 5.0f;

    private float DashRange = 0;


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
       

    }

    private void SearchEnemy()
    {
        if (shotingFlag) return;

        List<GameObject> targets = TargetEnemysInAngle();

        if (targets.Count <= 0) return;

        TargetGetAngle(targets);

    }
    //����AI�̎��E���ɓG�͂���̂��𔻒f����֐�
    private List<GameObject> TargetEnemysInAngle()
    {

        List<GameObject> targetObjcets = AIUtility.GetRelativeEnemy(PlayerFaction());

        List<GameObject> targets = new List<GameObject>();


        for (int i = 0; i < targetObjcets.Count; i++)
        {
            Vector3 vec = targetObjcets[i].transform.position - thisGameObject.transform.position;
            if (Vector3.Dot(thisGameObject.transform.forward, vec) < 0.8) continue;



            targets.Add(targetObjcets[i].gameObject);

        }


        return targets;



    }

    //���E���ɂ���G��ray���ʂ邩�𔻒f���Ċp�x�𓱂�
    private GameObject TargetGetAngle(List<GameObject> targets)
    {
        GameObject hitObject = null;

        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit hit;
            Vector3 startPosition = thisGameObject.transform.position + thisGameObject.transform.forward + RAYCAST_OFFSET[0];

            Debug.DrawLine(startPosition, targets[i].transform.position, Color.yellow);


            Vector3 dir = targets[i].transform.position - thisGameObject.transform.position;
            if (Physics.Raycast(startPosition, dir, out hit))
            {


                CharacterInsterface bullet = hit.transform.GetComponentInParent<CharacterInsterface>();


                if (bullet == null) continue;

                hitObject = hit.transform.gameObject;



            }


        }
        if (hitObject == null) return null;


        shotingFlag = true;
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

        return hitObject;

    }

    [SerializeField] private bool shotingFlag = false;
    public void Shot()
    {

        Vector3 startPos = thisGameObject.transform.position + thisGameObject.transform.forward / 10 + RAYCAST_OFFSET[0];
        RaycastHit hit;

        List<GameObject> targets = TargetEnemysInAngle();
        shotingFlag = false;

        if (targets.Count <= 0) return;

        GameObject targetObject = TargetGetAngle(targets);
        shotingFlag = false;
        if (targetObject == null) return;

        Vector3 Vec = targetObject.transform.position - thisGameObject.transform.position;

        Debug.DrawRay(startPos, Vec, Color.red, 1);

        if (Physics.Raycast(startPos, Vec, out hit))
        {
            GameObject ss = hit.transform.gameObject;
        }

        BulletMoveFunction.RayHitTest(startPos, Vec,PlayerFaction(),ID);

    }
    private void Wandering()
    {

        if (rotateFlag) daleyTime += Time.deltaTime;

        Vector3 startPos = thisGameObject.transform.position + thisGameObject.transform.forward / 10;

        for (int i = 0; i < RAYCAST_OFFSET.Length; i++)
        {
            Debug.DrawRay(startPos + RAYCAST_OFFSET[i], thisGameObject.transform.forward * AngleRange);

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

        if (nowMode == NowMode.Shot) AICharacterUtility.GetCharacterAI(ID).SetAnimatorTrigger("Shot");


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
                Debug.Log("Left");
                break;
            case ChengeAngleType.Right:
                AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Right", true);
                Debug.Log("Right");
                break;
        }


    }

    private GameObject hitObject;
    private void Back()
    {
        if (Vector3.Distance(hitObject.transform.position, thisGameObject.transform.position) > AngleRange) return;

        ResetAnimation();


        nextMode = NowMode.Wandering;

        nowMode = NowMode.ChageAngle;

        Vector3 vec = flag.transform.position - thisGameObject.transform.position;
        //���݂̊p�x
        float nowAngle = Mathf.Atan2(vec.x, vec.z) * Mathf.Rad2Deg;


        //�ύX�����p�x�̗�
        float angleVec = 0;

        Vector3 rayAngle = Vector3.zero;

        nextMoveAngle = 0;
        while (true)
        {
            angleVec++;

            RaycastHit hit;

            //�������̕����x�N�g��
            rayAngle.x = Mathf.Sin((nowAngle + angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle + angleVec) * Mathf.Deg2Rad);

            //�������̃��C
            if (Physics.Raycast(thisGameObject.transform.position + RAYCAST_OFFSET[0], rayAngle, out hit))
            {
                if (Vector3.Distance(thisGameObject.transform.position, hit.point) > ChengeAngleRange)
                {
                    if (hitObject != hit.transform.gameObject)
                        nextMoveAngle = nowAngle + angleVec;
                }
            }

            //�E�����̕����x�N�g��
            rayAngle.x = Mathf.Sin((nowAngle - angleVec) * Mathf.Deg2Rad);
            rayAngle.z = Mathf.Cos((nowAngle - angleVec) * Mathf.Deg2Rad);


            //�E�����̃��C
            if (Physics.Raycast(thisGameObject.transform.position + RAYCAST_OFFSET[0], rayAngle, out hit))
            {
                if (Vector3.Distance(thisGameObject.transform.position, hit.point) > ChengeAngleRange)
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
            //�ύX������������܂�����
            if (nextMoveAngle != 0) break;
        }


        ChangeLRAnime(ChengeAngleType.Left);

        nextMoveAngle += 360.0f;
        nextMoveAngle %= 360.0f;



        hitObject = null;
    }

    Vector3 endPosition = Vector3.zero;

    public void HitObject(Collision collision)
    {
        if (shotingFlag && nowMode != NowMode.Shot) 
        {
            shotingFlag = false;
            return;
        }

        if (collision.transform.tag == "Floor") return;

        endPosition = thisGameObject.transform.position + thisGameObject.transform.forward;

        hitObject = collision.gameObject;

        AICharacterUtility.GetCharacterAI(ID).SetAnimatorBool("Back", true);
        nowMode = NowMode.Back;

        thisGameObject.transform.LookAt(hitObject.transform);

        thisGameObject.transform.eulerAngles = new Vector3(0, thisGameObject.transform.eulerAngles.y, 0);

    }

    public void Resurrect()
    {
        ResetAnimation();

        Vector3 vec = playerFlag.transform.position - gameStartPosition;
        float angle = Mathf.Atan2(vec.x, vec.z);
        thisGameObject.transform.position = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle))*5 + playerFlag.transform.position;
        
        AICharacterUtility.GetCharacterAI(ID).SetISLife(true);

    }

    public void GroundSet()
    {
        Vector3 position = thisGameObject.transform.position;
        position.y = 0;
    }
}
