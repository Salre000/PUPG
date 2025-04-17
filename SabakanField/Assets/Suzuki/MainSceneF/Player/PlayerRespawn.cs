using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[����
public class PlayerRespawn:MonoBehaviour, CharacterInsterface
{
    // ���X�|�[���ʒu
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

    // �G����U�����󂯂���
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        PlayerManager.SetIsPlayerDead(true);
        RespawnManager.Instance.DelayRespawn(gameObject, _respawnPosition,_respawnTime);
    }

    // ��������
    private void RespawnComplete()
    {
        PlayerManager.SetIsPlayerDead(false);
        BulletManager.ResetMagazine();
        _timeCount = 0.0f;
    }

    // �������ԑ���
    private void RespawnTimeCount()
    {
        if(!PlayerManager.IsPlayerDead()) return;
        _timeCount += Time.deltaTime;
        // �������ʂ�ƃ��X�|�[���������Ƃ��킩��
        if( _timeCount >= _respawnTime)
            RespawnComplete();
    }

}
