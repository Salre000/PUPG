using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // �v���C���[�֘A

    //private float 

    // �v���C���[��(�G��)���͈͓̔��ɑ��݂��邩
    static public bool PlayerInFlagRange(Vector3 position) 
    {
        // �G���̊��I�u�W�F�N�g���擾
        GameObject goalFlag = CreateMapManager.GetFlag(1);
        if(goalFlag == null ) return false;

        // �����ƓG����
        if ((position - goalFlag.transform.position).sqrMagnitude <= 2.0f) { }


        return true;
    }


}
