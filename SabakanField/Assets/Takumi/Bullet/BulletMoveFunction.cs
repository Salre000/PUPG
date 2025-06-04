using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public static class BulletMoveFunction
{

    private static ObjectPool enemyPaintObjectPool;
    private static ObjectPool playerPaintObjectPool;

    private static ObjectPool aliceBulletObject;
    public static void SetPaint(GameObject enemy, GameObject player, GameObject Alice)
    {
        //弾が当ったときに出すプレハブのオブジェクト
        enemyPaintObjectPool = new ObjectPool(enemy, 50, "ParentEnemyPaint");
        playerPaintObjectPool = new ObjectPool(player, 50, "ParentPlayerPaint");
        aliceBulletObject = new ObjectPool(Alice, 10, "Alicepaint");
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


            MIssSoundPlay(new Ray(startPosition, dir), ID);


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
            dir.y += i / 70.0f;

            float angle = Mathf.Atan2(dir.x, dir.z) + BulletManager.GetRandomAngle(SHOTGAN_PELLET_COUNT / 2, SHOTGAN_PELLET_COUNT / 2);

            dir = new Vector3(Mathf.Sin(angle), dir.y, Mathf.Cos(angle));


            if (Physics.Raycast(startPosition, dir, out hit))
            {


                //当たった対象にrayが当ったときの関数を内包したインターフェースクラスが付いている場合取得
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //当たった対象に無敵の関数を内包したインターフェースクラスが付いている場合取得
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponent<InvincibleInsterface>();


                MIssSoundPlay(new Ray(startPosition, dir), ID);

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

    private const int PENETRARIONCOUNT = 4;
    //アリスの弾の挙動
    static public Vector3 AliceRayHitTest(Vector3 startPosition, Vector3 dir, bool playerFaction = true, int ID = 0)
    {
        RaycastHit hit;

        for (int i = 0; i < PENETRARIONCOUNT; i++)
        {
            if (Physics.Raycast(startPosition, dir, out hit))
            {

                Debug.DrawLine(startPosition, hit.point, Color.red,10);


                //当たった対象にrayが当ったときの関数を内包したインターフェースクラスが付いている場合取得
                CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();

                //当たった対象に無敵の関数を内包したインターフェースクラスが付いている場合取得
                InvincibleInsterface invincible = hit.transform.gameObject.GetComponentInParent<InvincibleInsterface>();


                MIssSoundPlay(new Ray(startPosition, dir), ID);


                //先の二つのインターフェースクラスが両方取得出来たかを判定
                if (hitObject == null && invincible == null)
                {
                    SetAlphaObject(hit.point, hit.normal, dir.normalized);

                    startPosition = hit.point + dir.normalized/10.0f; ;

                }
                else
                {
                    //当たった対象が無敵なのかを判定
                    if (invincible.GetInvincibleFlag()) return Vector3.zero;



                    //自分と同じ陣営の場合はフレンドリーファイアの関数を呼ぶ
                    if (hitObject.PlayerFaction() == playerFaction) hitObject.HitActionFriendlyFire();

                    //自分と違う陣営の場合は弾が当った時の処理を呼ぶ
                    else hitObject.HitAction();

                    //指定のIDのキャラクターのキルカウントを増やす
                    AIUtility.AddKillCount(ID);

                    return Vector3.zero;
                }

            }
            else
            {
                new GameObject("最終地点").transform.position = startPosition;
                return Vector3.zero;
            }


        }
        return Vector3.zero;
    }


    private static void SetPaintObject(Vector3 pos, Vector3 normal, Vector3 normalVec, bool playerFaction)
    {
        GameObject paintObject;
        if (playerFaction) paintObject = playerPaintObjectPool.GetObject();
        else paintObject = enemyPaintObjectPool.GetObject();

        //ペイントのプレハブを生成



        //rayが当たった法線を角度に変更
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //壁に埋まらないように少しだけ手前に出す
        paintObject.transform.position = pos - normalVec / 100.0f;


    }
    private static void SetAlphaObject(Vector3 pos, Vector3 normal, Vector3 normalVec)
    {
        GameObject paintObject;

        //透過オブジェクトのプレハブを生成
        paintObject = aliceBulletObject.GetObject();



        //rayが当たった法線を角度に変更
        Vector3 angle = Vector3.zero;
        angle.x = normal.z * 90;
        angle.z = normal.x * -90;
        paintObject.transform.eulerAngles = angle;

        //壁に埋まらないように少しだけ手前に出す
        paintObject.transform.position = pos - normalVec / 50.0f;


    }

    //弾が近くを通った敵に音をならす
    private static void MIssSoundPlay(Ray ray, int ID)
    {


        List<GameObject> list = AIUtility.GetChracterALL();

        for (int j = 0; j < list.Count; j++)
        {
            //音を聞く必要の無いAIは無視する
            if (j != 0) continue;

            //射撃した本人には聞こえないように
            if (j == ID) continue;

            //近くを通ったかどうかの判定
            if (DistanceToLine(ray, list[j].transform.position) > 2) continue;

            //音をならす
            //現在のプレイ人数は1人の為プレイヤーだけで良いが複数人になったら変更がいる

            SoundManager.StartSound(list[j].transform.position
                , SoundManager.GetInGameSoundList(SoundEnum.SoundEnumType._bulletSoundMissShot));



        }

    }

    public static float DistanceToLine(Ray ray, Vector3 point)
    {
        return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
    }

}