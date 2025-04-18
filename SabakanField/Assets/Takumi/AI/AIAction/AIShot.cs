using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShot 
{
    int ID = -1;
    public void SetID(int id) {ID= id;}

    GameObject thisGameObject;
    public void SetGameObject(GameObject game) {  thisGameObject = game; }

    public bool shotingFlag = false;
    public void SetShotFlag(bool flag) {  shotingFlag = flag; }

    GameObject ganObject;
    public void SetGanObject(GameObject gameObject) {  ganObject = gameObject; }

    private readonly float RANDOM_ANGLE = 45;

    public void Shot()
    {

        Vector3 startPos = ganObject.transform.position + ganObject.transform.forward;
        
        //デバッグ用
        Debug.DrawLine(startPos,startPos+(new Vector3()*10));
        Debug.DrawLine(startPos,startPos+(new Vector3()*10));



        RaycastHit hit;

        List<GameObject> targets =AICharacterFunction.TargetEnemysInAngle(ganObject, AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return;

        GameObject targetObject = AICharacterFunction.TargetGetAngle
            (targets,ID,thisGameObject, startPos);

        if (targetObject == null) return;

        Vector3 Vec = (targetObject.transform.position+Vector3.up) - startPos;

        Debug.DrawRay(startPos, Vec, Color.red, 1);

        if (Physics.Raycast(startPos, Vec, out hit))
        {
            GameObject ss = hit.transform.gameObject;
        }

        BulletMoveFunction.RayHitTest(startPos, Vec,AICharacterUtility.GetPlayerFaction(ID), ID);


    }


}