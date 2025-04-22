using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class OptionDataClass{

private  static  float  _test1;
public  static  float  Gettest1(){return _test1;}
private  static  float  _test2;
public  static  float  Gettest2(){return _test2;}
private  static  float  _test3;
public  static  float  Gettest3(){return _test3;}
private  static  float  _test4;
public  static  float  Gettest4(){return _test4;}private  static  string  _filePass=Application.dataPath;
private  static  string  _classname=CreateOptionData.ClassName;
private  static  string  _classExpansion=CreateOptionData.ClassExpansion;
private  static  string  _scvName=CreateOptionData.CSVName;
private  static  string  _FilePassReso=CreateOptionData.FilePass;

public static void OptionDataSave(float Settest1,float Settest2,float Settest3,float Settest4){

_test1=Settest1;
_test2=Settest2;
_test3=Settest3;
_test4=Settest4;
StreamWriter swSave;
swSave = new StreamWriter(_filePass+_FilePassReso+_scvName+_classExpansion,false);

swSave.WriteLine(_test1);
swSave.WriteLine(_test2);
swSave.WriteLine(_test3);
swSave.WriteLine(_test4);swSave.Flush();
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

_test1=float.Parse(csvDatas[0][0]);
_test2=float.Parse(csvDatas[1][0]);
_test3=float.Parse(csvDatas[2][0]);
_test4=float.Parse(csvDatas[3][0]);
}
public static void Initialization(){

_test1=0;
_test2=0;
_test3=0;
_test4=0;
}
}