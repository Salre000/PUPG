using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [Header("近接戦闘のマップオブジェクトリスト")]
    [SerializeField]private MapTile _CQBMap;
    [Header("陣地の近くのマップオブジェクトリスト")]
    [SerializeField]private MapTile _frackAreaMap;
    [Header("壁の多いのマップオブジェクトリスト")]
    [SerializeField]private MapTile _wallMap;
    [Header("障害物の少ないのマップオブジェクトリスト")]
    [SerializeField]private MapTile _natureMap;
    [Header("マップの原型のエクセルデータ")]
    public TextAsset _mapPlanAseet;

    //  マップの種類の列挙体
    enum MapTileType 
    {
        CQB=0,
        Frack,
        Wall,
        Nature
    }

    public void Awake()
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //for文用。一行目は読み込まない
        int i = 1;

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(_mapPlanAseet.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }

        int ss = 0;

    }



}
