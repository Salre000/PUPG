using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public static class BulletMoveFunction 
{

    private static GameObject enemyPaint;
    private static GameObject playerPaint;

    public static void SetPaint(GameObject enemy,GameObject player) 
    {
        enemyPaint = enemy;
        playerPaint = player;
    }

    //射撃の処理の関数・当たった対象にインターフェースクラスが付いている事が前提
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir,bool playerFaction=true,int ID=0)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {
          


            CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

            InvincibleInsterface invincible=hit.transform.gameObject.GetComponent<InvincibleInsterface>();


            if(hitObject==null||invincible==null) SetPaintObject(hit.point, hit.normal, dir.normalized);

            if (invincible.GetInvincibleFlag()) return Vector3.zero;



            //自分と同じ陣営の場合はフレンドリーファイアの関数を呼ぶ
            if (hitObject.PlayerFaction()== playerFaction) hitObject.HitActionFriendlyFire();

            //自分と違う陣営の場合は弾が当った時の処理を呼ぶ
            else hitObject.HitAction();

            AIUtility.AddKillCount(ID);

            return hit.point;
        }

        return Vector3.zero;
    }

    private static void SetPaintObject(Vector3 pos, Vector3 normal,Vector3 normalVec)
    {
        Debug.Log("弾が当ったときの壁の角度"+normal);

        GameObject paintObject = GameObject.Instantiate(enemyPaint);

        

        Vector3 angle = Vector3.zero;

        angle.x = normal.z * 90;
        angle.z = normal.x * -90;

        paintObject.transform.eulerAngles = angle;
        paintObject.transform.position = pos- normalVec/100.0f;


    }

}