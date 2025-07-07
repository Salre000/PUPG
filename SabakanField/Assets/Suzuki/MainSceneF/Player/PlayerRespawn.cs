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
        _respawnPosition = GameModes.mode==PublicEnum.GameMode.flag? AIUtility.GetFlagPosition():new Vector3(0,0,0);
        ChracterHPManager.instance.AddHP(100.0f);

        _respawnPosition.y += 0.1f;
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
    public void HitAction(GameObject Enemy = null)
    {
        DeathCamera.traget = Enemy;

        if (PlayerManager.GetIsArmor())
        {
            PlayerManager.SetIsArmor(false);
            return;
        }
        AIUtility.AddDeathCount();
        PlayerManager.SetIsPlayerDead(true);
        RespawnManager.Instance.DelayRespawn(gameObject, _respawnPosition, _respawnTime);
        _invincibleFlag = true;
    }
    public bool HPFaction(float damage) 
    {
        ChracterHPManager.instance.GetDamage(0, damage);

        return ChracterHPManager.instance.GetHp(0) <= 0;



    }


    // 復活完了
    private void RespawnComplete()
    {
        PlayerManager.SetIsPlayerDead(false);
        BulletManager.ResetMagazine(BulletManager.GetPlayerBulletMagazine());
        ChracterHPManager.instance.ResetHP(0);
        _timeCount = 0.0f;
    }

    // 復活時間測定
    private void RespawnTimeCount()
    {
        if (!PlayerManager.GetIsPlayerDead()) return;
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
