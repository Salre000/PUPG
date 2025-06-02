using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class OptionSaveData
{

    public enum adsType
    {
        None = -1,
        LongPress,
        Switch


    }

    private static adsType _nowAdsType; 

    public static bool GetAdsType() { return _nowAdsType.GetADSType();}


    private static float _BGMvolume = -1;
    public static float GetMGBVolume() {  return _BGMvolume; }

    private static float _SEvolume = -1;
    public static float GetSEVolume() {  return _SEvolume; }

    private static float _Mastervolume = -1;
    public static float GetMasterVolume() {  return _Mastervolume; }


    private static float _sensitivity = -1;
    public static float GetSensitivity() {  return _sensitivity; }

    private static float _ADSSensitivity = -1;
    public static float GetADSSensitivity() {  return _ADSSensitivity; }


    



    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME_OPTION = "OptionData";

    private static readonly string FILR_EXTENSION = ".csv";

    public static void OptionDataSave(bool adsFlag, float BGM, float MASTER, float seVolume, float sensitivity, float adsSensitivity)
    {
        //変数への代入
        Expansion.SetADSType(ref _nowAdsType,adsFlag);
        _BGMvolume= BGM;
        _Mastervolume = MASTER;
        _SEvolume = seVolume;
        _sensitivity = sensitivity;
        _ADSSensitivity = adsSensitivity;
        


        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FILE_NAME_OPTION + FILR_EXTENSION, false);

        sw.WriteLine(MASTER.ToString());
        sw.WriteLine(BGM.ToString());
        sw.WriteLine(seVolume.ToString());
        sw.WriteLine(sensitivity.ToString());
        sw.WriteLine(adsSensitivity.ToString());
        sw.WriteLine(adsFlag.ToString());
        sw.Flush();
        sw.Close();

    }

    public static void GetOptionData() 
    {
        bool adsFlag = false;
        GetOptionData(ref adsFlag,ref _BGMvolume,ref _Mastervolume,ref _SEvolume,
            ref _sensitivity,ref _ADSSensitivity);

        Expansion.SetADSType(ref _nowAdsType, adsFlag);


    }

    public static void GetOptionData(ref bool adsFlag, ref float bgmVolume, ref float masterVolume,
        ref float seVolume, ref float sensitivity, ref float adsSensitivity)
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();
        //CSVファイルの行数を格納
        int height = 0;


        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(FILE_NAME_OPTION);
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

        //全体の音量のフラグをcsvの情報を元に上書き
        masterVolume = float.Parse(csvDatas[0][0]);

        //BGMの音量をcsvの情報を元に上書き
        bgmVolume = float.Parse(csvDatas[1][0]);
        //SEの音量をcsvの情報を元に上書き
        seVolume = float.Parse(csvDatas[2][0]);

        //感度の情報を元に上書き
        sensitivity = float.Parse(csvDatas[3][0]);
        //ADS時の感度をcsvの情報を元に上書き
        adsSensitivity = float.Parse(csvDatas[4][0]);


        //ADSにの方法のフラグをcsvの情報を元に上書き
        adsFlag = bool.Parse(csvDatas[5][0]);


    }


}