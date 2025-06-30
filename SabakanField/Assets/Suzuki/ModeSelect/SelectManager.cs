using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    public enum Mode
    {
        none = -1,
        Flag,
        Death,
        Spike,
        max
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
        }
        Instance = this;
    }

    public void SelectMode(Mode mode)
    {
        string name = null;
        switch (mode)
        {
            case Mode.none:
                return;
            case Mode.Flag:
                name = GameSceneManager.flagScene;
                break;
            case Mode.Death:
                name = GameSceneManager.deathScene;
                break;
            case Mode.Spike:
                name = null;
                break;
        }
        if (name == null) return;
        GameSceneManager.LoadScene(name);
    }
}
