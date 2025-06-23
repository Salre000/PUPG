using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager:MonoBehaviour
{
    public static SelectManager Instance;

    private bool _flagSelect = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance==null)
        {
        }
            Instance = this;
    }

    public void SetFlagModeSelect(bool flag) { _flagSelect = flag; }
    public bool GetFlagModeSelect() { return _flagSelect; }

}
