
using UnityEngine;

public static class BulletManager
{
    // 弾関連

    // 銃に入る弾の上限
    private const int _LIMIT_BULLET = 30;
    // プレイヤーの銃に込められている弾の数
    static int playerBulletMagazine = 30;
    // 銃に込められている弾を除く、所有している残弾の最大数
    private const int _LIMIT_MAGAZIN = 120;
    static int bulletMagazin = 120;

    static public void Initialize()
    {
        playerBulletMagazine = _LIMIT_BULLET;
        bulletMagazin = _LIMIT_MAGAZIN;
    }

    // 撃った分だけ銃から弾を減らす
    static public void PlayerBulletShot(int ammo = 1)
    {
        playerBulletMagazine -= ammo;
    }
    static public void PlayerReload()
    {
        if (bulletMagazin <= 0) return;
        // 無駄なく弾を補充する
        int reloadBullet = 0;
        reloadBullet = _LIMIT_BULLET - playerBulletMagazine;
        bulletMagazin -= reloadBullet;
        // 残りの弾が30未満の場合
        if (bulletMagazin < 0)
        {
            reloadBullet += bulletMagazin;
            bulletMagazin = 0;
        }
        // リロード完了
        playerBulletMagazine += reloadBullet;

    }

    // 残弾チェック
    static public bool PlayerBulletMagazinCheck()
    {
        if (playerBulletMagazine > 0)
            return true;
        else
            return false;
    }
    static public int GetPlayerBulletMagazine() { return playerBulletMagazine; }
    static public int GetBulletMagazine() { return bulletMagazin; }

    // 銃と弾の補充
    static public void ResetMagazine() { playerBulletMagazine = _LIMIT_BULLET; bulletMagazin = _LIMIT_MAGAZIN; }
    // 弾の補充
    static public void SetMAXBulletMagazine() { bulletMagazin = _LIMIT_MAGAZIN; }


    ///ランダムに生成した値をラジアン角として返す関数（０に寄ることが多くなる）
    static public float GetRandomAngle(float times = 5, float random = 5)
    {

        float angle = 0;

        for (int i = 0; i < times; i++)
        {

            angle -= UnityEngine.Random.Range(0, random);
            angle += UnityEngine.Random.Range(0, random);
        }
        return angle *Mathf.Deg2Rad;
    }

}
