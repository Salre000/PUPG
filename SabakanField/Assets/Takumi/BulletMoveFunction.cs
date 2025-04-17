using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletMoveFunction 
{

    //�ˌ��̏����̊֐��E���������ΏۂɃC���^�[�t�F�[�X�N���X���t���Ă��鎖���O��
    static public Vector3 RayHitTest(Vector3 startPosition, Vector3 dir,bool playerFaction=true,int ID=0)
    {

        RaycastHit hit;

        if (Physics.Raycast(startPosition, dir, out hit))
        {

            CharacterInsterface hitObject = hit.transform.gameObject.GetComponentInParent<CharacterInsterface>();



            if (hitObject == null) return Vector3.zero;

            //�����Ɠ����w�c�̏ꍇ�̓t�����h���[�t�@�C�A�̊֐����Ă�
            if (hitObject.PlayerFaction()== playerFaction) hitObject.HitActionFriendlyFire();

            //�����ƈႤ�w�c�̏ꍇ�͒e�����������̏������Ă�
            else hitObject.HitAction();

            AIUtility.AddKillCount(ID);

            return hit.point;
        }

        return Vector3.zero;
    }

}