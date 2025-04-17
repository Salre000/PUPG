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
    /// ��������
    /// </summary>
    /// <param ������������GameObject="character"></param>
    /// <param ���X�|�[���n�_="_respawnPosition"></param>
    public void Respawn(GameObject character, Vector3 respawnPosition)
    {
        character.transform.position = respawnPosition;
    }
    /// <summary>
    /// �x��ĕ���
    /// </summary>
    /// <param ������������GameObject="character"></param>
    /// <param ���X�|�[���n�_="_respawnPosition"></param>
    /// <param �x�ꂳ���鎞��="resTime"></param>
    public void DelayRespawn(GameObject character, Vector3 respawnPosition, float resTime)
    {
        StartCoroutine(DelayRespawnCorotine(character, respawnPosition,resTime));
    }
    // �x���������s����
    private IEnumerator DelayRespawnCorotine(GameObject character, Vector3 respawnPosition,float resTime)
    {
        // �ꎞ��~���ł���������҂Ă�悤��
        yield return new WaitForSeconds(resTime);
        Respawn(character, respawnPosition);
    }


}
