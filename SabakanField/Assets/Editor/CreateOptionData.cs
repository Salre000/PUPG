using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CreateOptionData : EditorWindow
{
    public enum Type
    {
        Float,
        Bool,
        Int
    }


    [MenuItem("Assets/CreateOptiton")]
    static void CreateGUI()
    {

        This = ScriptableObject.CreateInstance<CreateOptionData>();

        optionName = Resources.Load<StringList>("stringList").objects;
        optiontype = Resources.Load<StringList>("stringList").type;

        This.Show();

    }
    static CreateOptionData This; 
    static List<string> optionName = new List<string>();
    static List<int> optiontype = new List<int>();
    private string _text;

    private void OnGUI()
    {

        if (GUILayout.Button("決定"))
        {
            CreateCS();
            This.Close();
        }

        // selecting sheets

        EditorGUILayout.LabelField("CreateOption");
        EditorGUILayout.BeginVertical("box");

        if (GUILayout.Button("一番最後に追加"))
        {
            optionName.Add("");
            optiontype.Add((int)Type.Float);

        }       
        if (GUILayout.Button("一番最後を消す"))
        {
            optionName.RemoveAt(optionName.Count-1); ;
            optiontype.RemoveAt(optiontype.Count-1);

        }
        GUILayout.EndVertical();

        for (int i = 0; i < optionName.Count; i++)
        {
            GUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("オプションの項目名");
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
        builder.Append("using System.Runtime.Serialization.Formatters.Binary;");
        builder.AppendLine();

        int size = optionName.Count;

        //�N���X�𐶐�
        builder.Append("[System.Serializable]");
        builder.AppendLine();
        builder.Append("public class OptionData{");
        builder.AppendLine();


        for (int i = 0; i < size; i++)
        {

            builder.Append("public ");
            builder.AppendFormat("{0} ", GetType(optiontype[i]));
            builder.AppendFormat("_{0};", optionName[i]);
            builder.AppendLine();

        }
        builder.Append("}");
            builder.AppendLine();


        builder.Append("public static class OptionDataClass{");
        builder.AppendLine();

        builder.Append("public static OptionData option;");

        for (int i = 0; i < size; i++)
        {
            builder.AppendLine();
            builder.AppendFormat("public  static  {0}  Get{1}()", GetType(optiontype[i]), optionName[i]);
            builder.Append("{");
            builder.AppendFormat("return option._{0}", optionName[i]);
            builder.Append(";}");
        }
        builder.Append("private  static  string  _filePass=Application.dataPath;");
        builder.AppendLine();

        builder.Append("private  static  string  _classname=Const.ClassName;");
        builder.AppendLine();

        builder.Append("private  static  string  _classExpansion=\".txt\";");
        builder.AppendLine();

        builder.Append("private  static  string  _scvName=Const.CSVName;");
        builder.AppendLine();

        builder.Append("private  static  string  _FilePassReso=Const.FilePass;");
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
            builder.AppendFormat("option._{0}=Set{0};", optionName[i]);

        }

        builder.AppendLine();
        builder.Append("BinaryFormatter formatter = new BinaryFormatter();");
        builder.AppendLine();
        builder.Append("string path = _filePass + _FilePassReso + _scvName+ _classExpansion;");
        builder.AppendLine();
        builder.Append(" FileStream stream = new FileStream(path, FileMode.OpenOrCreate);");
        builder.AppendLine();
        builder.Append(" OptionData data =option;");
        builder.AppendLine();
        builder.Append(" formatter.Serialize(stream, data);");
        builder.AppendLine();
        builder.Append(" stream.Close();");


        builder.AppendLine();
        builder.Append("}");
        builder.AppendLine();


        builder.Append("public static void GetOptionData(){");

        builder.AppendLine();
        builder.Append("string path = _filePass + _FilePassReso + _scvName + _classExpansion;");

        builder.AppendLine();
        builder.Append("if (File.Exists(path)){");

        builder.AppendLine();
        builder.Append("BinaryFormatter formatter = new BinaryFormatter();");

        builder.AppendLine();
        builder.Append(" FileStream stream = new FileStream(path, FileMode.Open);");


        builder.AppendLine();
        builder.Append("OptionData data = formatter.Deserialize(stream) as OptionData;");

        builder.AppendLine();
        builder.Append("option = data;");

        builder.AppendLine();
        builder.Append(" stream.Close();}");


        builder.AppendLine();
        builder.Append("else{");

        builder.AppendLine();
        builder.Append(" option = new OptionData();");

        builder.AppendLine();
        builder.Append("Initialization();");

        builder.AppendLine();
        builder.Append("}");
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
                builder.AppendFormat("option._{0}=false;", optionName[i]);
            }
            else if ((Type)(optiontype[i])==Type.Float)
            {
                builder.AppendFormat("option._{0}=0.5f;", optionName[i]);

            }
            else 
            {
                builder.AppendFormat("option._{0}=0;", optionName[i]);

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
            case 2: return "int";
        }
        return null;
    }

}