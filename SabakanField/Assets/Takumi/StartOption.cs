using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOption
{

    public static StartOption instance;

    public void Awake()
    {
        if (instance != null) return;
        if(instance==null)
         instance = this;

        OptionDataClass.GetOptionData();

    }
}
