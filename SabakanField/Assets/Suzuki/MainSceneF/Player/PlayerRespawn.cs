using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー復活
public class PlayerRespawn:MonoBehaviour, CharacterInsterface
{
    // リスポーン位置
    private Vector3 _respawnPosition;
    private float _respawnTime = 3.0f;
    private float _timeCount = 0.0f;

    private void Start()
    {

        _respawnPosition = AIUtility.GetFlagPosition();
        _respawnPosition.y += 1.0f;
    }

    private void Update()
    {
        RespawnTimeCount();
    }

    // 敵から攻撃を受けた時
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        PlayerManager.SetIsPlayerDead(true);
        RespawnManager.Instance.DelayRespawn(gameObject, _respawnPosition,_respawnTime);
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
        if(!PlayerManager.IsPlayerDead()) return;
        _timeCount += Time.deltaTime;
        // ここが通るとリスポーンしたことがわかる
        if( _timeCount >= _respawnTime)
            RespawnComplete();
    }

}
