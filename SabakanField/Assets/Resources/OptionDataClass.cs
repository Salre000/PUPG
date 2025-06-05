using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class OptionData
{
    public float _NormalSensitivity;
    public float _AdsSensitivity;
    public bool _AdsType;
    public int _MasterVolume;
    public int _BGMVolume;
    public int _SEVolume;
}
public static class OptionDataClass
{
    public static OptionData option;
    public static float GetNormalSensitivity() { return option._NormalSensitivity; }
    public static float GetAdsSensitivity() { return option._AdsSensitivity; }
    public static bool GetAdsType() { return option._AdsType; }
    public static int GetMasterVolume() { return option._MasterVolume; }
    public static int GetBGMVolume() { return option._BGMVolume; }
    public static int GetSEVolume() { return option._SEVolume; }
    private static string _filePass = Application.dataPath;
    private static string _classname = Const.ClassName;
    private static string _classExpansion = ".txt";
    private static string _scvName = Const.CSVName;
    private static string _FilePassReso = Const.FilePass;

    public static void OptionDataSave(float SetNormalSensitivity, float SetAdsSensitivity, bool SetAdsType, int SetMasterVolume, int SetBGMVolume, int SetSEVolume)
    {

        option._NormalSensitivity = SetNormalSensitivity;
        option._AdsSensitivity = SetAdsSensitivity;
        option._AdsType = SetAdsType;
        option._MasterVolume = SetMasterVolume;
        option._BGMVolume = SetBGMVolume;
        option._SEVolume = SetSEVolume;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = _filePass + _FilePassReso + _scvName + _classExpansion;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        OptionData data = option;
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void GetOptionData()
    {
        string path = _filePass + _FilePassReso + _scvName + _classExpansion;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            OptionData data = formatter.Deserialize(stream) as OptionData;
            option = data;
            stream.Close();
        }
        else
        {
            option = new OptionData();
            Initialization();
        }
    }
    public static void Initialization()
    {

        option._NormalSensitivity = 0.5f;
        option._AdsSensitivity = 0.5f;
        option._AdsType = false;
        option._MasterVolume = 0;
        option._BGMVolume = 0;
        option._SEVolume = 0;
    }
}