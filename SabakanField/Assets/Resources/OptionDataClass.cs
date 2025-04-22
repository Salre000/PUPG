using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class OptionDataClass{

private  static  float  _Sensitivity;
public  static  float  GetSensitivity(){return _Sensitivity;}private  static  string  _filePass=Application.dataPath;
private  static  string  _classname= Const.ClassName;
private  static  string  _classExpansion= Const.ClassExpansion;
private  static  string  _scvName= Const.CSVName;
private  static  string  _FilePassReso= Const.FilePass;

public static void OptionDataSave(float SetSensitivity){

_Sensitivity=SetSensitivity;
StreamWriter swSave;
swSave = new StreamWriter(_filePass+_FilePassReso+_scvName+_classExpansion,false);

swSave.WriteLine(_Sensitivity);swSave.Flush();
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

_Sensitivity=float.Parse(csvDatas[0][0]);
}
public static void Initialization(){

_Sensitivity=0;
}
}