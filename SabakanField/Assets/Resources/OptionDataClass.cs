using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class OptionDataClass{

private  static  float  _NormalSensitivity;
public  static  float  GetNormalSensitivity(){return _NormalSensitivity;}
private  static  float  _AdsSensitivity;
public  static  float  GetAdsSensitivity(){return _AdsSensitivity;}
private  static  bool  _AdsType;
public  static  bool  GetAdsType(){return _AdsType;}
private  static  int  _MasterVolume;
public  static  int  GetMasterVolume(){return _MasterVolume;}
private  static  int  _BGMVolume;
public  static  int  GetBGMVolume(){return _BGMVolume;}
private  static  int  _SEVolume;
public  static  int  GetSEVolume(){return _SEVolume;}private  static  string  _filePass=Application.dataPath;
private  static  string  _classname=Const.ClassName;
private  static  string  _classExpansion=Const.ClassExpansion;
private  static  string  _scvName=Const.CSVName;
private  static  string  _FilePassReso=Const.FilePass;

public static void OptionDataSave(float SetNormalSensitivity,float SetAdsSensitivity,bool SetAdsType,int SetMasterVolume,int SetBGMVolume,int SetSEVolume){

_NormalSensitivity=SetNormalSensitivity;
_AdsSensitivity=SetAdsSensitivity;
_AdsType=SetAdsType;
_MasterVolume=SetMasterVolume;
_BGMVolume=SetBGMVolume;
_SEVolume=SetSEVolume;
StreamWriter swSave;
swSave = new StreamWriter(_filePass+_FilePassReso+_scvName+_classExpansion,false);

swSave.WriteLine(_NormalSensitivity);
swSave.WriteLine(_AdsSensitivity);
swSave.WriteLine(_AdsType);
swSave.WriteLine(_MasterVolume);
swSave.WriteLine(_BGMVolume);
swSave.WriteLine(_SEVolume);swSave.Flush();
swSave.Close();
}
public static void GetOptionData(){
List<string[]> csvDatas = new List<string[]>();
int height = 0;
TextAsset textAsset = Resources.Load<TextAsset>(_scvName);
if(textAsset==null){Initialization();return;}
StringReader reader = new StringReader(textAsset.text);
while (reader.Peek() > -1){
string line = reader.ReadLine();
csvDatas.Add(line.Split(','));
height++;
}

_NormalSensitivity=float.Parse(csvDatas[0][0]);
_AdsSensitivity=float.Parse(csvDatas[1][0]);
_AdsType=bool.Parse(csvDatas[2][0]);
_MasterVolume=int.Parse(csvDatas[3][0]);
_BGMVolume=int.Parse(csvDatas[4][0]);
_SEVolume=int.Parse(csvDatas[5][0]);
}
public static void Initialization(){

_NormalSensitivity=0.5f;
_AdsSensitivity=0.5f;
_AdsType=false;
_MasterVolume=0;
_BGMVolume=0;
_SEVolume=0;
}
}