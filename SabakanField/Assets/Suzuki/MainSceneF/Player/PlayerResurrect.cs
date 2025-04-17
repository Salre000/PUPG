using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー復活
public class PlayerResurrect:MonoBehaviour, CharacterInsterface
{
    // リスポーン位置
    private Vector3 resurrectPosition;

    private void Start()
    {
        resurrectPosition = AIUtility.GetFlagPosition();
        resurrectPosition.y += 1.0f;
    }

    // 敵から攻撃を受けた時
    public void HitAction()
    {
        AIUtility.AddDeathCount();
        transform.position = resurrectPosition;
    }



}
