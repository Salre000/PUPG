using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[����
public class PlayerResurrect:MonoBehaviour, CharacterInsterface
{
    // ���X�|�[���ʒu
    private Vector3 resurrectPosition;

    private void Start()
    {
        resurrectPosition = AIUtility.GetFlagPosition();
        resurrectPosition.y += 1.0f;
    }

    // �G����U�����󂯂���
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        transform.position = resurrectPosition;
    }



}
