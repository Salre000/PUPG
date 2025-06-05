using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
//データをCSVに保存するクラス
[System.Serializable]

public static class KillData
{

    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME_KD = "SaveData";

    private static readonly string FILR_EXTENSION = ".csv";

    public static void InGameDataSave(KIllCount kIll)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, kIll);
        stream.Close();

    }
    public static KIllCount InGameDataLoad()
    {
        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        if (!File.Exists(path))return new KIllCount();
        

            BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        KIllCount data = formatter.Deserialize(stream) as KIllCount;
        stream.Close();

        return data;
    }


}
