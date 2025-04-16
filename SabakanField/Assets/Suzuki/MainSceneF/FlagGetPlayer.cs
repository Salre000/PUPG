using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{
    private float count = 0.0f;
    private bool _flagCheck = false;

    private void Update()
    {
        // Šø‚Ì”ÍˆÍ“à‚É‚¢‚é‚È‚ç
        if (PlayerManager.PlayerInFlagRange(transform.position)) 
            FlagGetCheck();
        else
        {
            _flagCheck = false;
        }
        GameManager.Instance.SetFlagRangeCheck(_flagCheck);
    }

    private void FlagGetCheck()
    {
        if(UIManager.Instance.GetOverLimitTime()) return;
        _flagCheck =true;
        count+=1*Time.deltaTime;
        UIManager.Instance.FlagCountGage(count);
        return;
    }

}
