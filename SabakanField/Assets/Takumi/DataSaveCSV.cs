using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class DataSaveCSV
{

    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME= "InGameData /SaveData";

    private static readonly string FILR_EXTENSION = ".csv";

   public static void InGameDataSave(string []kill,string []death)
    {
        string Kill =string.Join(",", kill);
        string Death = string.Join(",", death);
        StreamWriter sw; 

        sw = new StreamWriter(Application.dataPath + FILE_PASS+ FILE_NAME+ FILR_EXTENSION, false);

        sw.WriteLine(Kill);
        
        sw.WriteLine(Death);

        sw.Write(UIManager.Instance.GetTime().ToString());

        sw.Flush();

        sw.Close();

    }
}
