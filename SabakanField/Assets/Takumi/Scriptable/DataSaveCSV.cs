using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
//データをCSVに保存するクラス
public static class DataSaveCSV
{

    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME_KD = "SaveData";

    private static readonly string FILR_EXTENSION = ".csv";

    public static void InGameDataSave(string[] kill, string[] death)
    {
        string Kill = string.Join(",", kill);
        string Death = string.Join(",", death);
        StreamWriter sw;
        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append(FILE_PASS);
        builder.Append(FILE_NAME_KD);
        builder.Append(FILR_EXTENSION);
        sw = new StreamWriter(Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION, false);
        //sw = new StreamWriter(builder.ToString(), false);

        sw.WriteLine(Kill);

        sw.WriteLine(Death);

        sw.Write(UIManager.Instance.GetTime().ToString());

        sw.Flush();

        sw.Close();

    }

}
