using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{
    // ���݉�%��
    private float _count = 0.0f;
    // 1�b�����艽%���₷��
    private float _countSpeed = 1.0f;
    // ���͈͓̔��ɂ��邩
    private bool _flagCheck = false;

    private void Update()
    {
        // ���͈͓̔��ɂ���Ȃ�
        if (PlayerManager.PlayerInFlagRange(transform.position))
            FlagGetCheck();
        else
            _flagCheck = false;
        GameManager.Instance.SetFlagRangeCheck(_flagCheck);
    }

    private void FlagGetCheck()
    {
        // �Q�[�W��100%�������̓f�X��Ԃ͑����Ȃ��悤�ɂ���
        if (UIManager.Instance.GetOverTime() ||
            UIManager.Instance.GageMaxCheck()||
            PlayerManager.IsPlayerDead()) return;
        _flagCheck = true;
        _count += _countSpeed * Time.deltaTime;
        UIManager.Instance.FlagCountGage(_count);
    }

}
