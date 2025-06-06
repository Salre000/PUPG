using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInsterface
{
    virtual public bool PlayerFaction() { return true; }


    //弾が当ったときの処理
    abstract public void HitAction();

    //味方から弾を当てられた時の処理（何もしなければ弾が当った時と同じ）
    virtual public void HitActionFriendlyFire() { HitAction(); }

}
