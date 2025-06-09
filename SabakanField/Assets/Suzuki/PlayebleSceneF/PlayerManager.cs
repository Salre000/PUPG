using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // プレイヤー関連

    // 死亡確認
    private static bool _isDead = false;
    // どのくらいの差でtrueにするかの値
    private const float _POSITION_MAGNITUDE = 5.0f;
    // ADS中か
    private static bool _isAds = false;
    // アーマーを装着しているか
    private static bool _isArmor=false;

    // プレイヤーが(敵の)旗の範囲内に存在するか
    static public bool PlayerInFlagRange(Vector3 position) 
    {
        // 敵側の旗オブジェクトを取得
        GameObject goalFlag = CreateMapManager.GetFlag(1);
        if(goalFlag == null ) return false;

        // 引数と敵旗が定数以下の距離なら
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
