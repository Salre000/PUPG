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

    public void Shot()
    {

        Vector3 startPos = ganObject.transform.position + ganObject.transform.forward;

        List<GameObject> targets =AICharacterFunction.TargetEnemysInAngle(ganObject, AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return;

        GameObject targetObject = AICharacterFunction.TargetGetAngle
            (targets,ID,thisGameObject, startPos);

        if (targetObject == null) return;

        Vector3 Vec = (targetObject.transform.position) - startPos;

        float angle = Mathf.Atan2(Vec.x, Vec.z);

        angle += GetRandomAngle();

        Vec = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        BulletMoveFunction.RayHitTest(startPos, Vec,AICharacterUtility.GetPlayerFaction(ID), ID);


    }
    float GetRandomAngle()
    {

        float angle = 0;
        for (int i = 0; i < 5; i++)
        {

            angle -= UnityEngine.Random.Range(0, 5);
            angle += UnityEngine.Random.Range(0, 5);
        }

        return angle * Mathf.Deg2Rad;


    }

}