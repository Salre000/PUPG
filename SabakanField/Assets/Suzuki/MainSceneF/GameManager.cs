using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �Q�[���̐i�s�Ǘ�

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameClearCheck(bool clearFlag)
    {

    }

}
