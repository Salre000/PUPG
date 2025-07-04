using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour, InvincibleInsterface,CharacterInsterface
{
    public bool GetInvincibleFlag()
    {

        return false;
    }

    public void HitAction(GameObject Enemy = null)
    {
        Debug.Log("サンドバッグに当った");
    }

    bool CharacterInsterface.HPFaction(float damage)
    {
        return true;
    }
}
