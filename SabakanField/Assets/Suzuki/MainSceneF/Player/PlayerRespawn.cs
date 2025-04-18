using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー復活
public class PlayerRespawn : MonoBehaviour, CharacterInsterface, InvincibleInsterface
{
    // リスポーン位置
    private Vector3 _respawnPosition;
    // リスポーンまでの時間
    private float _respawnTime = 3.0f;
    // リスポーンしたか検知するための時間把握
    private float _timeCount = 0.0f;
    // 復活してからの無敵時間
    private float _invincibleTime = 2.0f;
    // 無敵かどうか
    private bool _invincibleFlag = false;

    private void Start()
    {
        _respawnPosition = AIUtility.GetFlagPosition();
        _respawnPosition.y += 1.0f;
        transform.position = _respawnPosition;
    }

    private void Update()
    {
        // デバッグ無敵
        if (Input.GetKeyDown(KeyCode.M))
            if (_invincibleFlag) _invincibleFlag = false; else _invincibleFlag = true;

        RespawnTimeCount();
    }

    // 敵から攻撃を受けた時
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        PlayerManager.SetIsPlayerDead(true);
        RespawnManager.Instance.DelayRespawn(gameObject, _respawnPosition, _respawnTime);
        _invincibleFlag = true;
    }

    // 復活完了
    private void RespawnComplete()
    {
        PlayerManager.SetIsPlayerDead(false);
        BulletManager.ResetMagazine();
        _timeCount = 0.0f;
    }

    // 復活時間測定
    private void RespawnTimeCount()
    {
        if (!PlayerManager.IsPlayerDead()) return;
        _timeCount += Time.deltaTime;
        // ここが通るとリスポーンしたことがわかる
        if (_timeCount >= _respawnTime)
        {
            RespawnComplete();
            StartCoroutine(RespoawnInvincible());
        }
    }

    private IEnumerator RespoawnInvincible()
    {
        // 指定秒数分待機してから無敵解除(死亡判定も解除)
        yield return new WaitForSeconds(_invincibleTime);
        _invincibleFlag = false;
    }

    // 死んでる間と復活して1秒は無敵
    public bool GetInvincibleFlag() { return _invincibleFlag; }
}
