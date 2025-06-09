using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // �v���C���[�֘A

    // ���S�m�F
    private static bool _isDead = false;
    // �ǂ̂��炢�̍���true�ɂ��邩�̒l
    private const float _POSITION_MAGNITUDE = 5.0f;
    // ADS����
    private static bool _isAds = false;
    // �A�[�}�[�𑕒����Ă��邩
    private static bool _isArmor=false;

    // �v���C���[��(�G��)���͈͓̔��ɑ��݂��邩
    static public bool PlayerInFlagRange(Vector3 position) 
    {
        // �G���̊��I�u�W�F�N�g���擾
        GameObject goalFlag = CreateMapManager.GetFlag(1);
        if(goalFlag == null ) return false;

        // �����ƓG�����萔�ȉ��̋����Ȃ�
        if ((position - goalFlag.transform.position).sqrMagnitude <= _POSITION_MAGNITUDE)
        { 
            return true;
        }

        ////
        GameObject gan = GanObject.constancyGun.objects[(int)GanObject.ConstancyGanType.SL_8];

        ////
        return false;
    }

    public static bool GetIsPlayerDead() { return _isDead; }
    public static void SetIsPlayerDead(bool flag) {  _isDead = flag; }

    public static bool GetIsAds() { return _isAds; }
    public static void SetIsAds(bool flag) { _isAds = flag; }
    public static bool GetIsArmor() { return _isArmor; }
    public static void SetIsArmor(bool flag) { _isArmor = flag; }

}
