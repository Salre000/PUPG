using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class StartOption
{

    public static StartOption instance;

    public StartOption()
    {
        if (instance != null) return;

        if (instance == null) instance = this;

        OptionDataClass.GetOptionData();
        //第一引数がバイナリーデータ　第二引数が
        //BitConverter.ToString();
    }
}
