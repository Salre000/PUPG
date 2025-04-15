using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // �e�ɓ���e�̏��
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
        reloadBullet = _LIMIT_BULLET - playerBulletMagazine;
        bulletMagazin -= reloadBullet;
        // �c��̒e��30�����̏ꍇ
        if(bulletMagazin < 0)
        {
            reloadBullet+=bulletMagazin;
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

    // �v���C���[��(�G��)���͈͓̔��ɑ��݂��邩
    static public bool PlayerInFlagRange(Vector3 position) 
    {

        return true;
    }

    static public int GetPlayerBulletMagazine() { return playerBulletMagazine; }
    static public int GetBulletMagazine() { return bulletMagazin; }
}
