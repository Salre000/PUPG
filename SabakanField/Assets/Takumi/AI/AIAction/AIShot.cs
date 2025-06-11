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

    GanObject.ConstancyGanType ganType;

    public void SetGanType(GanObject.ConstancyGanType type) {  ganType = type; }
    public void Shot(float range)
    {

        Vector3 startPos = ganObject.transform.position + ganObject.transform.forward;

        List<GameObject> targets =AICharacterFunction.TargetEnemysInAngle(ganObject, AICharacterUtility.GetPlayerFaction(ID));

        if (targets.Count <= 0) return;

        GameObject targetObject = AICharacterFunction.TargetGetAngle
            (targets,ID,thisGameObject, startPos);

        if (targetObject == null) return;

        Vector3 Vec = (targetObject.transform.position) - startPos;

        float angle = Mathf.Atan2(Vec.x, Vec.z);

        angle +=BulletManager.GetRandomAngle(range, range);

        Vec = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

        Debug.DrawRay(startPos, Vec*30,Color.white,2);

        //SoundManager.StartSound(startPos, SoundManager.GetShotSound(ganType),
        //    () =>
        //    {
        //        Vector3 pos = startPos;
        //        pos.y = 0;
        //        SoundManager.StartSound(pos, SoundManager.GetShotAddSound(3));
        //    }
        //    );
        //BulletMoveFunction.RayHitTest(startPos, Vec,AICharacterUtility.GetPlayerFaction(ID), ID);


    }

}