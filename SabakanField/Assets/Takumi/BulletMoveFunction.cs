using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletMoveFunction 
{

    //射撃の処理の関数・当たった対象にインターフェースクラスが付いている事が前提
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir,bool playerFaction=true)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {

            BulletMove hitObject = hit.transform.gameObject.GetComponentInParent<BulletMove>();


            if (hitObject == null) return Vector3.zero;

            //自分と同じ陣営の場合はフレンドリーファイアの関数を呼ぶ
            if (hitObject.PlayerFaction()== playerFaction) hitObject.HitActionFriendlyFire();

            //自分と違う陣営の場合は弾が当った時の処理を呼ぶ
            else hitObject.HitAction();

            return hit.point;
        }
        return Vector3.zero;
    }

}