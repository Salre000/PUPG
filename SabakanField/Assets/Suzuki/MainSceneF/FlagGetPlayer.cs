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

    private bool FlagGetCheck()
    {
        UIManager.Instance.num+=1*Time.deltaTime;
        return true;
    }

}
