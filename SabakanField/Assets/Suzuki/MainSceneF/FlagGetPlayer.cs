using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{

    private void Update()
    {
        // ���͈͓̔��ɂ���Ȃ�
        if (PlayerManager.PlayerInFlagRange(transform.position)) 
            FlagGetCheck();
    }

    private void FlagGetCheck()
    {
        Debug.Log("���̎��͂ɂ��܂�");
    }

}
