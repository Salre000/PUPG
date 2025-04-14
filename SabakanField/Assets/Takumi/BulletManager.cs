using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletManager 
{

    //�ˌ��̏����̊֐��E���������ΏۂɃC���^�[�t�F�[�X�N���X���t���Ă��鎖���O��
    static public void RayHitTest(Vector3 startPosition, Vector3 dir,bool playerFaction=true)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {

            BulletMove hitObject = hit.transform.gameObject.GetComponentInParent<BulletMove>();

            if (hitObject == null) return;

            //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
            if (hitObject.PlayerFaction()== playerFaction) hitObject.HitActionFriendlyFire();

            //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
            else hitObject.HitAction();
        }
    }

}