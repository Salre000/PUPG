using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public static class BulletMoveFunction
{

    private static PaintObjectPool enemyPaintObjectPool;
    private static PaintObjectPool playerPaintObjectPool;
    public static void SetPaint(GameObject enemy, GameObject player)
    {
        //弾が当ったときに出すプレハブのオブジェクト
        enemyPaintObjectPool = new PaintObjectPool(enemy, 50, "ParentEnemyPaint");
        playerPaintObjectPool = new PaintObjectPool(player, 50, "ParentPlayerPaint");
    }

    //射撃の処理の関数・当たった対象にインターフェースクラスが付いている事が前提
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {


            //当たった対象にrayが当ったときの関数を内包したインターフェースクラスが付いている場合取得
            CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

            //当たった対象に無敵の関数を内包したインターフェースクラスが付いている場合取得
            InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();

            //先の二つのインターフェースクラスが両方取得出来たかを判定
            if (hitObject == null && invincible == null) { SetPaintObject(hit.point, hit.normal, dir.normalized, playerFaction); return Vector3.zero; }

            //当たった対象が無敵なのかを判定
            if (invincible.GetInvincibleFlag()) return Vector3.zero;



            //自分と同じ陣営の場合はフレンドリーファイアの関数を呼ぶ
            if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

            //自分と違う陣営の場合は弾が当った時の処理を呼ぶ
            else hitObject.HitAction();

            //指定のIDのキャラクターのキルカウントを増やす
            AIUtility.AddKillCount(ID);

            return hit.point;
        }

        return Vector3.zero;
    }
    private const int SHOTGAN_PELLET_COUNT = 8;

    //ショットガンの弾の挙動
    static public Vector3 ShotgunRayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {
        Vector3 LoodAngle = dir;

        for (int i = -SHOTGAN_PELLET_COUNT / 2; i < SHOTGAN_PELLET_COUNT / 2; i++)
        {
            dir = LoodAngle;
            RaycastHit hit;
            dir.y += i/70.0f;

            float angle = Mathf.Atan2(dir.x, dir.z)+BulletManager.GetRandomAngle(SHOTGAN_PELLET_COUNT/2, SHOTGAN_PELLET_COUNT/2);

            dir = new Vector3(Mathf.Sin(angle), dir.y, Mathf.Cos(angle));


            if (Physics.Raycast(startPosition, dir, out hit))
            {


                //当たった対象にrayが当ったときの関数を内包したインターフェースクラスが付いている場合取得
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //当たった対象に無敵の関数を内包したインターフェースクラスが付いている場合取得
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();

                //先の二つのインターフェースクラスが両方取得出来たかを判定
                if (hitObject == null && invincible == null) { SetPaintObject(hit.point, hit.normal, dir.normalized, playerFaction); continue; }

                //当たった対象が無敵なのかを判定
                if (invincible.GetInvincibleFlag()) continue;



                //自分と同じ陣営の場合はフレンドリーファイアの関数を呼ぶ
                if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

                //自分と違う陣営の場合は弾が当った時の処理を呼ぶ
                else hitObject.HitAction();

                //指定のIDのキャラクターのキルカウントを増やす
                AIUtility.AddKillCount(ID);

                return hit.point;
            }

        }
        return Vector3.zero;
    }
    private static void SetPaintObject(Vector3 pos, Vector3 normal, Vector3 normalVec, bool playerFaction)
    {
        GameObject paintObject;
        if (playerFaction) paintObject = playerPaintObjectPool.GetpaintObject();
        else paintObject = enemyPaintObjectPool.GetpaintObject();

        //ペイントのプレハブを生成



        //rayが当たった法線を角度に変更
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //壁に埋まらないように少しだけ手前に出す
        paintObject.transform.position = pos - normalVec / 100.0f;


    }

}