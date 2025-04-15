using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    // プレイヤー関連

    //private float 

    // プレイヤーが(敵の)旗の範囲内に存在するか
    static public bool PlayerInFlagRange(Vector3 position) 
    {
        // 敵側の旗オブジェクトを取得
        GameObject goalFlag = CreateMapManager.GetFlag(1);
        if(goalFlag == null ) return false;

        // 引数と敵旗が
        if ((position - goalFlag.transform.position).sqrMagnitude <= 2.0f) { }


        return true;
    }


}
