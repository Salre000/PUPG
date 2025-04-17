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
        string Kill =string.Join(",", kill);
        string Death = string.Join(",", death);
        StreamWriter sw; 

        sw = new StreamWriter(Application.dataPath + FILE_PASS, false);

        sw.WriteLine(Kill);
        
        sw.WriteLine(Death);

        sw.Write(UIManager.Instance.GetTime().ToString());


        sw.Close();

    }
}
