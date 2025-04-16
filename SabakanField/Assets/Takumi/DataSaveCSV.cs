using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class DataSaveCSV
{

    private static readonly string FILE_PASS = "/Resources/InGameData/SaveData.csv";


   public static void InGameDataSave(string []kill,string []death)
    {
        StreamWriter sw; 

        sw = new StreamWriter(Application.dataPath + FILE_PASS, false);

        sw.Write(kill);
        sw.Write(death);
        sw.Write(UIManager.Instance.GetTime());


        sw.Close();

    }
}
