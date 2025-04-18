using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[����
public class PlayerRespawn : MonoBehaviour, CharacterInsterface, InvincibleInsterface
{
    // ���X�|�[���ʒu
    private Vector3 _respawnPosition;
    // ���X�|�[���܂ł̎���
    private float _respawnTime = 3.0f;
    // ���X�|�[�����������m���邽�߂̎��Ԕc��
    private float _timeCount = 0.0f;
    // �������Ă���̖��G����
    private float _invincibleTime = 2.0f;
    // ���G���ǂ���
    private bool _invincibleFlag = false;

    private void Start()
    {
        _respawnPosition = AIUtility.GetFlagPosition();
        _respawnPosition.y += 1.0f;
        transform.position = _respawnPosition;
    }

    private void Update()
    {
        // �f�o�b�O���G
        if (Input.GetKeyDown(KeyCode.M))
            if (_invincibleFlag) _invincibleFlag = false; else _invincibleFlag = true;

        RespawnTimeCount();
    }

    // �G����U�����󂯂���
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        PlayerManager.SetIsPlayerDead(true);
        RespawnManager.Instance.DelayRespawn(gameObject, _respawnPosition, _respawnTime);
        _invincibleFlag = true;
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
        if (!PlayerManager.IsPlayerDead()) return;
        _timeCount += Time.deltaTime;
        // �������ʂ�ƃ��X�|�[���������Ƃ��킩��
        if (_timeCount >= _respawnTime)
        {
            RespawnComplete();
            StartCoroutine(RespoawnInvincible());
        }
    }

    private IEnumerator RespoawnInvincible()
    {
        // �w��b�����ҋ@���Ă��疳�G����(���S���������)
        yield return new WaitForSeconds(_invincibleTime);
        _invincibleFlag = false;
    }

    // ����ł�Ԃƕ�������1�b�͖��G
    public bool GetInvincibleFlag() { return _invincibleFlag; }
}
