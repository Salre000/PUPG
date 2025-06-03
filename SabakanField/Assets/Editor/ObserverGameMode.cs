using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ObserverGameMode : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Resources/";
    private static readonly string filePath2 = "MapTileName";
    public static string FILR_EXTENSION = ".asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        string filename = filePath +filePath2+ FILR_EXTENSION;
        foreach (string asset in importedAssets)
        {
            if (!filename.Equals(asset))
                continue;

            CreateCS();






        }
    }
    private static void CreateCS()
    {
        MapPlanDataObject achievementsAll =Resources.Load<MapPlanDataObject>(filePath2);

        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(Application.dataPath);
        builder.Append("/Resources/MAPEnum");
        builder.Append(".cs");

        StreamWriter sw;

        string filePass = builder.ToString();
        sw = new StreamWriter(filePass, false);
        builder.Clear();
        builder.Append("public  static class MapTypeEnum {");
        builder.AppendLine();


        builder.Append("public enum MapType {");
        builder.AppendLine();

        for (int i = 0; i < achievementsAll.GetCount(); i++)
        {
            builder.AppendFormat("//{0}", achievementsAll.GetMapTileEX(i));

            builder.AppendLine();

            builder.AppendFormat("_{0}", achievementsAll.GetMapTileName(i));
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
