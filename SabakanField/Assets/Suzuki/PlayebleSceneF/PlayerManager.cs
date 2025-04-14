using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    private const int _LIMIT_BULLET = 30;
    // �v���C���[�̏e�ɍ��߂��Ă���e�̐�
    static int playerBulletMagazine = 30;
    // �e�ɍ��߂��Ă���e�������A���L���Ă���c�e�̍ő吔
    static int bulletMagazin = 120;

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
        if (bulletMagazin < 30)
        {
            reloadBullet = bulletMagazin;
            bulletMagazin = 0;
        }
        else
        {
            reloadBullet = _LIMIT_BULLET - playerBulletMagazine;
            bulletMagazin -= reloadBullet;
        }
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
}
