using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, CharacterInsterface
{
    public void HitAction()
    {
        AIUtility.AddDeathCount();

        //リスポーンの処理

        this.transform.position = AIUtility.GetFlagPosition();


    }
}
