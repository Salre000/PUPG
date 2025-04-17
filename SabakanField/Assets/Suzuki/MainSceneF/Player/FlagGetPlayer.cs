using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{
    // Œ»İ‰½%‚©
    private float _count = 0.0f;
    // 1•b“–‚½‚è‰½%‘‚â‚·‚©
    private float _countSpeed = 1.0f;
    // Šø‚Ì”ÍˆÍ“à‚É‚¢‚é‚©
    private bool _flagCheck = false;

    private void Update()
    {
        // Šø‚Ì”ÍˆÍ“à‚É‚¢‚é‚È‚ç
        if (PlayerManager.PlayerInFlagRange(transform.position))
            FlagGetCheck();
        else
            _flagCheck = false;
        GameManager.Instance.SetFlagRangeCheck(_flagCheck);
    }

    private void FlagGetCheck()
    {
        if (UIManager.Instance.GetOverLimitTime()) return;
        _flagCheck = true;
        _count += _countSpeed * Time.deltaTime;
        UIManager.Instance.FlagCountGage(_count);
    }

}
