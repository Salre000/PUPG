using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelect : MonoBehaviour
{
    [SerializeField] private Button _flagModeButton;

    // Start is called before the first frame update
    void Awake()
    {
        _flagModeButton.onClick.AddListener(() => OnFlagMode());
    }

    
    private void OnFlagMode()
    {
        GameSceneManager.LoadScene(GameSceneManager.flagScene);
    }
}
