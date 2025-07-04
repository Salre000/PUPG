using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterInsterface
{
    virtual public bool PlayerFaction() { return true; }

    //€‚Ê‚Æ‚«‚Étrue‚ğ•Ô‚·ŠÖ”
    abstract public bool HPFaction(float damage);

    //’e‚ª“–‚Á‚½‚Æ‚«‚Ìˆ—
    abstract public void HitAction(GameObject Enemy=null);

    //–¡•û‚©‚ç’e‚ğ“–‚Ä‚ç‚ê‚½‚Ìˆ—i‰½‚à‚µ‚È‚¯‚ê‚Î’e‚ª“–‚Á‚½‚Æ“¯‚¶j
    virtual public void HitActionFriendlyFire() { HitAction(); }

}
