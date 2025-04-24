
public static class BulletManager
{
    // �e�֘A

    // �e�ɓ���e�̏��
    private const int _LIMIT_BULLET = 30;
    // �v���C���[�̏e�ɍ��߂��Ă���e�̐�
    static int playerBulletMagazine = 30;
    // �e�ɍ��߂��Ă���e�������A���L���Ă���c�e�̍ő吔
    private const int _LIMIT_MAGAZIN = 120;
    static int bulletMagazin = 120;

    static public void Initialize()
    {
        playerBulletMagazine = _LIMIT_BULLET;
        bulletMagazin = _LIMIT_MAGAZIN;
    }

    // �������������e����e�����炷
    static public void PlayerBulletShot(int ammo = 1)
    {
        playerBulletMagazine -= ammo;
    }
    static public void PlayerReload()
    {
        if (bulletMagazin <= 0) return;
        // ���ʂȂ��e���[����
        int reloadBullet = 0;
        reloadBullet = _LIMIT_BULLET - playerBulletMagazine;
        bulletMagazin -= reloadBullet;
        // �c��̒e��30�����̏ꍇ
        if (bulletMagazin < 0)
        {
            reloadBullet += bulletMagazin;
            bulletMagazin = 0;
        }
        // �����[�h����
        playerBulletMagazine += reloadBullet;

    }

    // �c�e�`�F�b�N
    static public bool PlayerBulletMagazinCheck()
    {
        if (playerBulletMagazine > 0)
            return true;
        else
            return false;
    }
    static public int GetPlayerBulletMagazine() { return playerBulletMagazine; }
    static public int GetBulletMagazine() { return bulletMagazin; }

    // �e�ƒe�̕�[
    static public void ResetMagazine() { playerBulletMagazine = _LIMIT_BULLET; bulletMagazin = _LIMIT_MAGAZIN; }
    // �e�̕�[
    static public void SetMAXBulletMagazine() { bulletMagazin = _LIMIT_MAGAZIN; }

}
