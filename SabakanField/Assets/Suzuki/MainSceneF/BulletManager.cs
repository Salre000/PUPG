
using InfimaGames.LowPolyShooterPack;
using System.Collections;
using UnityEngine;

public static class BulletManager
{
    // ’eŠÖ˜A

    // e‚É“ü‚é’e‚ÌãŒÀ
    private static int _LIMIT_BULLET = 30;
    // ƒvƒŒƒCƒ„[‚Ìe‚É‚ß‚ç‚ê‚Ä‚¢‚é’e‚Ì”
    static int playerBulletMagazine = 30;
    static int allBulletMagazine;
    // e‚É‚ß‚ç‚ê‚Ä‚¢‚é’e‚ğœ‚­AŠ—L‚µ‚Ä‚¢‚éc’e‚ÌÅ‘å”
    private static int _limitMagazin;
    static int bulletMagazin = -1;

    static public void Initialize()
    {
        //_limitMagazin=GetBulletMagazine();
        playerBulletMagazine = _LIMIT_BULLET;
        bulletMagazin = _limitMagazin;
        SetMAXMagazine(allBulletMagazine);
    }

    // Œ‚‚Á‚½•ª‚¾‚¯e‚©‚ç’e‚ğŒ¸‚ç‚·
    static public void PlayerBulletShot(int ammo = 1)
    {
        playerBulletMagazine -= ammo;
    }
    static public void PlayerReload()
    {
        if (bulletMagazin <= 0) return;
        // –³‘Ê‚È‚­’e‚ğ•â[‚·‚é
        int reloadBullet = 0;
        reloadBullet = bulletMagazin - playerBulletMagazine;
        bulletMagazin -= reloadBullet;
        // c‚è‚Ì’e‚ª30–¢–‚Ìê‡
        if (bulletMagazin < 0)
        {
            reloadBullet += bulletMagazin;
            bulletMagazin = 0;
        }
        // ƒŠƒ[ƒhŠ®—¹
        playerBulletMagazine += reloadBullet;

    }

    // c’eƒ`ƒFƒbƒN
    static public bool PlayerBulletMagazinCheck()
    {
        if (playerBulletMagazine > 0)
            return true;
        else
            return false;
    }

    private static int _ammunition;
    private static int _magazin;


    /// <summary>
    /// </summary>
    /// <param ƒ}ƒKƒWƒ““à‚Ì’e”="magazin"></param>
    /// <param e‚É“ü‚Á‚Ä‚¢‚é‹…”="ammunition"></param>
    /// <param e‚É“ü‚éŒÀŠE‚Ì’e”="max"></param>
    static public void ReloadSystem(int magazin, int ammunition, int max)
    {
        // ƒŠƒ[ƒh‚·‚é’e”‚ğŒˆ’è‚·‚é
        if (magazin < max)
        {
            int num = max;
            max = magazin+ammunition;
            if (num < max)
                max = num;
        }
        int value = max - ammunition;
        // ‘’e”‚©‚çƒŠƒ[ƒh‚µ‚½•ª‚ğˆø‚­
        magazin -= value;
        // e‚É’e‚ğ•â[
        ammunition += value;

        _ammunition = ammunition;
        _magazin = magazin;
    }



    static public int GetAmmunition() { return _ammunition; }
    static public int GetMagazin() { return _magazin; }
    static public void SetMagazin(int value) { _magazin = value; }

    static public int GetPlayerBulletMagazine() { return playerBulletMagazine; }
    static public int GetBulletMagazine() { return bulletMagazin; }

    // e‚Æ’e‚Ì•â[
    static public void ResetMagazine(int value) { playerBulletMagazine = value; bulletMagazin = value; }
    // ’e‚Ì•â[
    static public void SetMAXBulletMagazine(int value) { bulletMagazin = value; }
    static public void SetAllBulletMagazine(int value) { allBulletMagazine = value; }
    // ƒvƒŒƒCƒ„[‚Ì\
    static public void SetPlayerBulletMagazine(int value) { playerBulletMagazine = value; }
    static public void SetMAXMagazine(int value) { _LIMIT_BULLET = value * 3; SetMAXBulletMagazine(_LIMIT_BULLET); }


    ///ƒ‰ƒ“ƒ_ƒ€‚É¶¬‚µ‚½’l‚ğƒ‰ƒWƒAƒ“Šp‚Æ‚µ‚Ä•Ô‚·ŠÖ”i‚O‚ÉŠñ‚é‚±‚Æ‚ª‘½‚­‚È‚éj
    static public float GetRandomAngle(float times = 5, float random = 5)
    {

        float angle = 0;

        for (int i = 0; i < times; i++)
        {

            angle -= UnityEngine.Random.Range(0, random);
            angle += UnityEngine.Random.Range(0, random);
        }
        return angle * Mathf.Deg2Rad;
    }

}
