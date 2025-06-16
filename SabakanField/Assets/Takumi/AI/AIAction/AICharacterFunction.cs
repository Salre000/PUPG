using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIMove;

public static class AICharacterFunction 
{
    public static List<GameObject> TargetEnemysInAngle(GameObject thisGameObject,bool isEnemy)
    {

        List<GameObject> targetObjcets = AIUtility.GetRelativeEnemy(isEnemy);

        List<GameObject> targets = new List<GameObject>();


        for (int i = 0; i < targetObjcets.Count; i++)
        {
            Vector3 vec = targetObjcets[i].transform.position;
            vec-= thisGameObject.transform.position;
            if (Vector3.Dot(thisGameObject.transform.forward, vec) < 0.8) continue;





            targets.Add(targetObjcets[i].gameObject);

        }


        return targets;



    }

    public static GameObject TargetGetAngle(List<GameObject> targets,int ID, GameObject thisGameObject,Vector3 startPos)
    {
        GameObject hitObject = null;

        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit hit;


            Vector3 dir = targets[i].transform.position - thisGameObject.transform.position;
            if (Physics.Raycast(startPos, dir, out hit))
            {


                CharacterInsterface bullet = hit.transform.GetComponentInParent<CharacterInsterface>();


                if (bullet == null) continue;
                if (bullet.PlayerFaction() == AICharacterUtility.GetPlayerFaction(ID)) continue;
                if (Vector3.Distance(startPos, hit.point) > 100) continue;

                Debug.Log("ƒŒƒ“ƒW" + Vector3.Distance(startPos, hit.point));

                hitObject = hit.transform.gameObject;
            }

        }
        if (hitObject == null) return null;

        return hitObject;
    }

}