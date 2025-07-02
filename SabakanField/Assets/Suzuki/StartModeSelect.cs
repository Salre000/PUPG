using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class StartModeSelect : MonoBehaviour
{

    void Update()
    {
        ModeTransition();    
    }

    private void ModeTransition()
    {
        if (!Input.anyKeyDown) return;
        gameObject.SetActive(false);
    }
}
