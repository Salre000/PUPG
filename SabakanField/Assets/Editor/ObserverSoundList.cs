using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ObserverSoundList : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/";
    private static readonly string filePath2 = "SoundList";
    public static string FILR_EXTENSION = ".asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string filename = filePath + filePath2 + FILR_EXTENSION;
        foreach (string asset in importedAssets)
        {
            if (!filename.Equals(asset))
                continue;

            CreateCS();






        }
    }
    private static void CreateCS()
    {
        SoundList ALL = Resources.Load<SoundList>(filePath2);

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Resources/SoundEnum");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("public  static class SoundEnum {");
        builder.AppendLine();


        builder.Append("public enum SoundEnumType {");
        builder.AppendLine();

        for (int i = 0; i < ALL.SoundLists.Count; i++)
        {
            builder.AppendFormat("        /// <summary><see _{0}=\"{1}\"/> </summary>\r\n", ALL.SoundName[i], ALL.Explanation[i]);

            builder.AppendLine();

            builder.AppendFormat("_{0}", ALL.SoundName[i]);
            builder.Append(",");
            builder.AppendLine();

        }

        builder.Append("MAX");
        builder.AppendLine();


        builder.Append("}");

        builder.AppendLine();

        builder.Append("}");

        sw.Write(builder.ToString());

        sw.Close();
    }



}
