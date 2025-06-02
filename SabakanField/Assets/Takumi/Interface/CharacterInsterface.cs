using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInsterface
{
    virtual public bool PlayerFaction() { return true; }


    //’e‚ª“–‚Á‚½‚Æ‚«‚Ìˆ—
    abstract public void HitAction();

    //–¡•û‚©‚ç’e‚ğ“–‚Ä‚ç‚ê‚½‚Ìˆ—i‰½‚à‚µ‚È‚¯‚ê‚Î’e‚ª“–‚Á‚½‚Æ“¯‚¶j
    virtual public void HitActionFriendlyFire() { HitAction(); }

}
