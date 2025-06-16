using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour,CharacterInsterface
{
    public void HitAction()
    {
        print("Hit Wall");
        return;
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
