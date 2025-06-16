using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, CharacterInsterface
{
    public void HitAction()
    {
        AIUtility.AddDeathCount();

        //ƒŠƒXƒ|[ƒ“‚Ìˆ—

        this.transform.position = AIUtility.GetFlagPosition();


    }

    void CharacterInsterface.HitAction()
    {
        throw new System.NotImplementedException();
    }

    bool CharacterInsterface.HPFaction(float damage)
    {
        return false;
    }
}
