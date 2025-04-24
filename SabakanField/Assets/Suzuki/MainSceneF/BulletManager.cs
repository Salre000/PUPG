
public static class BulletManager
{
    // ’eŠÖ˜A

    // e‚É“ü‚é’e‚ÌãŒÀ
    private const int _LIMIT_BULLET = 30;
    // ƒvƒŒƒCƒ„[‚Ìe‚É‚ß‚ç‚ê‚Ä‚¢‚é’e‚Ì”
    static int playerBulletMagazine = 30;
    // e‚É‚ß‚ç‚ê‚Ä‚¢‚é’e‚ğœ‚­AŠ—L‚µ‚Ä‚¢‚éc’e‚ÌÅ‘å”
    private const int _LIMIT_MAGAZIN = 120;
    static int bulletMagazin = 120;

    static public void Initialize()
    {
        playerBulletMagazine = _LIMIT_BULLET;
        bulletMagazin = _LIMIT_MAGAZIN;
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
        reloadBullet = _LIMIT_BULLET - playerBulletMagazine;
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
    static public int GetPlayerBulletMagazine() { return playerBulletMagazine; }
    static public int GetBulletMagazine() { return bulletMagazin; }

    // e‚Æ’e‚Ì•â[
    static public void ResetMagazine() { playerBulletMagazine = _LIMIT_BULLET; bulletMagazin = _LIMIT_MAGAZIN; }
    // ’e‚Ì•â[
    static public void SetMAXBulletMagazine() { bulletMagazin = _LIMIT_MAGAZIN; }

}
