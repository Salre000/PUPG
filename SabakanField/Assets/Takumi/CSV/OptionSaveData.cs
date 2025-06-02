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
        //�ϐ��ւ̑��
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
        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();
        //CSV�t�@�C���̍s�����i�[
        int height = 0;


        //�q�����t�@�C���p�X���g���t�@�C���̃��[�h���s��
        TextAsset textAsset = Resources.Load<TextAsset>(FILE_NAME_OPTION);
        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
        }
        //csvData�̒��Ƀf�[�^���i�[����Ă���

        //�S�̂̉��ʂ̃t���O��csv�̏������ɏ㏑��
        masterVolume = float.Parse(csvDatas[0][0]);

        //BGM�̉��ʂ�csv�̏������ɏ㏑��
        bgmVolume = float.Parse(csvDatas[1][0]);
        //SE�̉��ʂ�csv�̏������ɏ㏑��
        seVolume = float.Parse(csvDatas[2][0]);

        //���x�̏������ɏ㏑��
        sensitivity = float.Parse(csvDatas[3][0]);
        //ADS���̊��x��csv�̏������ɏ㏑��
        adsSensitivity = float.Parse(csvDatas[4][0]);


        //ADS�ɂ̕��@�̃t���O��csv�̏������ɏ㏑��
        adsFlag = bool.Parse(csvDatas[5][0]);


    }


}