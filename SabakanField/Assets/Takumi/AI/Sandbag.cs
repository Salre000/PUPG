using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour, InvincibleInsterface,CharacterInsterface
{
    public bool GetInvincibleFlag()
    {

        return false;
    }

    public void HitAction()
    {
        Debug.Log("“–‚Á‚½");
    }

    void CharacterInsterface.HitAction()
    {
        throw new System.NotImplementedException();
    }

    bool CharacterInsterface.HPFaction(float damage)
    {
        throw new System.NotImplementedException();
    }
}
