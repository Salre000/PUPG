using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
    [Header("マップの原型のエクセルデータの名前が入っているオブジェクト")]
    [SerializeField] private MapPlanDataObject _planData;

    private readonly string _PLAN_PASS = "MapPlanData/";

    private readonly int _MAX_SIZE = 6;

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
        StringBuilder builder = new StringBuilder();

        builder.Clear();

        builder.Append(_PLAN_PASS);
        builder.Append(_planData.GetMapTileName(0));


        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }

        int mapReto = 10;

        for(int x=0;x< _MAX_SIZE; x++) 
        {
            for (int z = 0; z < _MAX_SIZE; z++)
            {
                int MapTypeNumber = int.Parse(csvDatas[x][z]);

                GameObject mapTile = GetRandomMapTile((MapTileType)MapTypeNumber);

                mapTile.transform.localPosition = new Vector3(x * mapReto, 10, z * mapReto);

                int randAngle = Random.Range(0, 4);

                mapTile.transform.eulerAngles = new Vector3(0, 90*randAngle,0);
            }
        }
    }

    private GameObject GetRandomMapTile(MapTileType mapType)
    {

        int randomNumber =-1;

        switch (mapType)
        {
            case MapTileType.CQB:
                randomNumber=Random.Range(0,_CQBMap.mapTileMax);
                return GameObject.Instantiate(_CQBMap.GetMapTile(randomNumber));

            case MapTileType.Frack:
                randomNumber = Random.Range(0, _frackAreaMap.mapTileMax);
                return GameObject.Instantiate(_frackAreaMap.GetMapTile(randomNumber));

            case MapTileType.Wall:
                randomNumber = Random.Range(0,_wallMap.mapTileMax);
                return GameObject.Instantiate(_wallMap.GetMapTile(randomNumber));

            case MapTileType.Nature:
                randomNumber = Random.Range(0, _natureMap.mapTileMax);
                return GameObject.Instantiate(_natureMap.GetMapTile(randomNumber));

        }

        return null;



    }



}
