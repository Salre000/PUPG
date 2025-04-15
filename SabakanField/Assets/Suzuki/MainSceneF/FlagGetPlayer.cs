using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGetPlayer : MonoBehaviour
{

    private void Update()
    {
        // Šø‚Ì”ÍˆÍ“à‚É‚¢‚é‚È‚ç
        if (PlayerManager.PlayerInFlagRange(transform.position)) 
            FlagGetCheck();
    }

    private void FlagGetCheck()
    {
        Debug.Log("Šø‚ÌŽüˆÍ‚É‚¢‚Ü‚·");
    }

}
