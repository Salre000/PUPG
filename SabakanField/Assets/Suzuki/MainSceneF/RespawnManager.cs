using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private void Awake()
    {
        if (Instance != this)
            Instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// 即時復活
    /// </summary>
    /// <param 復活させたいGameObject="character"></param>
    /// <param リスポーン地点="_respawnPosition"></param>
    public void Respawn(GameObject character, Vector3 respawnPosition)
    {
        character.transform.position = respawnPosition;
    }
    /// <summary>
    /// 遅れて復活
    /// </summary>
    /// <param 復活させたいGameObject="character"></param>
    /// <param リスポーン地点="_respawnPosition"></param>
    /// <param 遅れさせる時間="resTime"></param>
    public void DelayRespawn(GameObject character, Vector3 respawnPosition, float resTime)
    {
        StartCoroutine(DelayRespawnCorotine(character, respawnPosition,resTime));
    }
    // 遅延復活実行処理
    private IEnumerator DelayRespawnCorotine(GameObject character, Vector3 respawnPosition,float resTime)
    {
        // 一時停止中でもしっかり待てるように
        yield return new WaitForSeconds(resTime);
        Respawn(character, respawnPosition);
    }


}
