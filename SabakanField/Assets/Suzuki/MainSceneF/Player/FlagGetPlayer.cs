using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{
    // 現在何%か
    private float _count = 0.0f;
    // 1秒当たり何%増やすか
    private float _countSpeed = 1.0f;
    // 旗の範囲内にいるか
    private bool _flagCheck = false;

    private void Update()
    {
        // 旗の範囲内にいるなら
        if (PlayerManager.PlayerInFlagRange(transform.position))
            FlagGetCheck();
        else
            _flagCheck = false;
        GameManager.Instance.SetFlagRangeCheck(_flagCheck);
    }

    private void FlagGetCheck()
    {
        // ゲージが100%もしくはデス状態は増えないようにする
        if (UIManager.Instance.GetOverTime() ||
            UIManager.Instance.GageMaxCheck()||
            PlayerManager.IsPlayerDead()) return;
        _flagCheck = true;
        _count += _countSpeed * Time.deltaTime;
        UIManager.Instance.FlagCountGage(_count);
    }

}
