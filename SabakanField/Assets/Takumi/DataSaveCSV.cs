using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;
//�f�[�^��CSV�ɕۑ�����N���X
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
        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
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
        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();
        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append(FILE_PASS);
        builder.Append(FILE_NAME_OPTION);

        //�q�����t�@�C���p�X���g���t�@�C���̃��[�h���s��
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());
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

        //ADS�ɂ̕��@�̃t���O��csv�̏������ɏ㏑��
        adsFlag = bool.Parse(csvDatas[0][0]);

        //BGM�̉��ʂ�csv�̏������ɏ㏑��
        bgmVolume = float.Parse(csvDatas[1][0]);

        //�S�̂̉��ʂ̃t���O��csv�̏������ɏ㏑��
        masterVolume = float.Parse(csvDatas[2][0]);

    }
}
