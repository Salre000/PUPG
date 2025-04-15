using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{
    private float count = 0.0f;

    private void Update()
    {
        // ���͈͓̔��ɂ���Ȃ�
        if (PlayerManager.PlayerInFlagRange(transform.position)) 
            FlagGetCheck();
    }

    private bool FlagGetCheck()
    {
        count+=1*Time.deltaTime;
        UIManager.Instance.FlagCountGage(count);
        return true;
    }

}
