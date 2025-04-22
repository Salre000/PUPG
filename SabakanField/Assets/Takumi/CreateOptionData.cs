using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CreateOptionData : EditorWindow
{
    public enum Type
    {
        Float,
        Bool
    }

    public static string ClassName = "/Resources/OptionDataClass";
    public static string FilePass = "/Resources/";
    public static string CSVName = "OptionDataCSV";
    public static string ClassExpansion = ".csv";

    [MenuItem("Assets/CreateOptiton")]
    static void CreateGUI()
    {

        var window = ScriptableObject.CreateInstance<CreateOptionData>();

        optionName = Resources.Load<StringList>("stringList").objects;
        optiontype = Resources.Load<StringList>("stringList").type;

        window.Show();

    }
    static List<string> optionName = new List<string>();
    static List<int> optiontype = new List<int>();
    private string _text;

    private void OnGUI()
    {

        if (GUILayout.Button("Œˆ’è"))
        {
            CreateCS();

        }

        // selecting sheets

        EditorGUILayout.LabelField("sheet settings");
        EditorGUILayout.BeginVertical("box");

        if (GUILayout.Button("’Ç‰Á"))
        {
            optionName.Add("");
            optiontype.Add((int)Type.Float);

        }
        GUILayout.EndVertical();

        for (int i = 0; i < optionName.Count; i++)
        {
            GUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ƒIƒvƒVƒ‡ƒ“‚Ì€–Ú–¼");
            EditorGUILayout.EndHorizontal();
            GUILayout.EndVertical();


            GUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            optionName[i] = EditorGUILayout.TextField(optionName[i]);

            optiontype[i] = (int)(Type)EditorGUILayout.EnumPopup((Type)optiontype[i]);


            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }


    }

    private void CreateCS()
    {
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Resources/OptionDataClass");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);

        builder.Clear();

        builder.Append("using UnityEngine;");
        builder.AppendLine();
        builder.Append("using System.IO;");
        builder.AppendLine();
        builder.Append("using System.Collections.Generic;");
        builder.AppendLine();
        builder.Append("public static class OptionDataClass{");
        builder.AppendLine();

        int size = optionName.Count;

        for (int i = 0; i < size; i++)
        {
            builder.AppendLine();
            builder.AppendFormat("private  static  {0}  _{1};", GetType(optiontype[i]), optionName[i]);
            builder.AppendLine();
            builder.AppendFormat("public  static  {0}  Get{1}()", GetType(optiontype[i]), optionName[i]);
            builder.Append("{");
            builder.AppendFormat("return _{0}", optionName[i]);
            builder.Append(";}");
        }
        builder.Append("private  static  string  _filePass=Application.dataPath;");
        builder.AppendLine();

        builder.Append("private  static  string  _classname=CreateOptionData.ClassName;");
        builder.AppendLine();

        builder.Append("private  static  string  _classExpansion=CreateOptionData.ClassExpansion;");
        builder.AppendLine();

        builder.Append("private  static  string  _scvName=CreateOptionData.CSVName;");
        builder.AppendLine();

        builder.Append("private  static  string  _FilePassReso=CreateOptionData.FilePass;");
        builder.AppendLine();

        builder.AppendLine();

        builder.Append("public static void OptionDataSave(");
        for (int i = 0; i < size; i++)
        {

            builder.AppendFormat("{0} Set{1}", GetType(optiontype[i]), optionName[i]);

            if (i < size - 1) builder.Append(",");
        }
        builder.Append("){");

        builder.AppendLine();


        for (int i = 0; i < size; i++)
        {

            builder.AppendLine();
            builder.AppendFormat("_{0}=Set{0};", optionName[i]);

        }

        builder.AppendLine();


        builder.Append("StreamWriter swSave;");
        builder.AppendLine();

        builder.Append("swSave = new StreamWriter(_filePass+_FilePassReso+_scvName+_classExpansion,false);");
        builder.AppendLine();

        for (int i = 0; i < size; i++)
        {
            builder.AppendLine();

            builder.AppendFormat("swSave.WriteLine(_{0});", optionName[i].ToString());

        }
    


        builder.Append("swSave.Flush();");
        builder.AppendLine();
        builder.Append("swSave.Close();");
        builder.AppendLine();
        builder.Append("}");
        builder.AppendLine();


        builder.Append("public static void GetOptionData(){");

        builder.AppendLine();

        builder.Append("List<string[]> csvDatas = new List<string[]>();");
        builder.AppendLine();
        builder.Append("int height = 0;");
        builder.AppendLine();


        builder.Append("TextAsset textAsset = Resources.Load<TextAsset>(_scvName);");

        builder.AppendLine();

        builder.Append("if(textAsset==null){Initialization();");
        builder.Append("return;}");
        builder.AppendLine();
        builder.Append("StringReader reader = new StringReader(textAsset.text);");
        builder.AppendLine();
        builder.Append("while (reader.Peek() > -1){");
        builder.AppendLine();
        builder.Append("string line = reader.ReadLine();");
        builder.AppendLine();
        builder.Append("csvDatas.Add(line.Split(','));");
        builder.AppendLine();
        builder.Append("height++;");
        builder.AppendLine();
        builder.Append("}");
        builder.AppendLine();
        for (int i = 0; i < size; i++)
        {
            builder.AppendLine();

            builder.AppendFormat("_{0}={1}.Parse(csvDatas[{2}][0]);", optionName[i], GetType(optiontype[i]), i.ToString());

        }

        builder.AppendLine();
        builder.Append("}");
        builder.AppendLine();

        builder.Append("public static void Initialization(){");
        builder.AppendLine();
        for (int i = 0; i < size; i++)
        {
            builder.AppendLine();
            if ((Type)optiontype[i] == Type.Bool)
            {
                builder.AppendFormat("_{0}=false;", optionName[i]);
            }
            else
            {
                builder.AppendFormat("_{0}=0;", optionName[i]);

            }
        }

        builder.AppendLine();
        builder.Append("}");
        builder.AppendLine();
        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();
    }


    string GetType(int number)
    {
        switch (number)
        {
            case 0: return "float";
            case 1: return "bool";
        }
        return null;
    }

}