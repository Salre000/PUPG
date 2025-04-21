using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
//データをCSVに保存するクラス
public static class DataSaveCSV
{

    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME_KD = "InGameData /SaveData";
    public const string FILE_NAME_OPTION = "OptionData/OptionData";

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

        sw = new StreamWriter(builder.ToString(), false);

        sw.WriteLine(Kill);

        sw.WriteLine(Death);

        sw.Write(UIManager.Instance.GetTime().ToString());

        sw.Flush();

        sw.Close();

    }

    public static void OptionDataSave(bool adsFlag,float BGM,float MASTER)
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FILE_NAME_OPTION + FILR_EXTENSION, false);

        sw.WriteLine(adsFlag.ToString());
        
        sw.WriteLine(BGM.ToString());

        sw.WriteLine(BGM.ToString());

        sw.Flush();
        sw.Close();

    }

    public static void GetOptionData(ref bool adsFlag, ref float bgmVolume, ref float masterVolume)
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();
        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append(FILE_PASS);
        builder.Append(FILE_NAME_OPTION);

        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());
        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }
        //csvDataの中にデータが格納されている

        //ADSにの方法のフラグをcsvの情報を元に上書き
        adsFlag = bool.Parse(csvDatas[0][0]);

        //BGMの音量をcsvの情報を元に上書き
        bgmVolume = float.Parse(csvDatas[1][0]);

        //全体の音量のフラグをcsvの情報を元に上書き
        masterVolume = float.Parse(csvDatas[2][0]);

    }
}
