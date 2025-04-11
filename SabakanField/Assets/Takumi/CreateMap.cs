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
    [SerializeField]private MapTile _flagAreaMap;
    [Header("壁の多いのマップオブジェクトリスト")]
    [SerializeField]private MapTile _wallMap;
    [Header("障害物の少ないのマップオブジェクトリスト")]
    [SerializeField]private MapTile _natureMap;
    [Header("マップの原型のエクセルデータの名前が入っているオブジェクト")]
    [SerializeField] private MapPlanDataObject _planData;
    [Header("マップに配置するフラッグのモデルオブジェクト")]
    [SerializeField] private GameObject _flagObjectBase;
    [Header("フラッグのマテリアル、プレイヤー、エネミーの順番")]
    [SerializeField]　private Material []_flagMaterial=new Material[2];
    [Header("マップの範囲外と区切る壁のプレハブ")]
    [SerializeField] private GameObject _wallObject;

    private readonly string _PLAN_PASS = "MapPlanData/";

    const int _MAX_SIZE = 6;

    const float _MAP_HEIGHT = 0.5f;
    const int MAP_RETO = 10;


    private readonly Vector3 _ENEMYFLAG_POSITION = new Vector3((MAP_RETO*_MAX_SIZE)-MAP_RETO, _MAP_HEIGHT, (MAP_RETO * _MAX_SIZE) - MAP_RETO);
    private readonly Vector3 _PLAYERFLAG_POSITION = new Vector3((MAP_RETO / 2), _MAP_HEIGHT, (MAP_RETO / 2));

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
        //地面と障害物を生成する関数
        CreateGraund();

        //フラッグの生成する関数
        CreateFlag();

        CreateMapWall();
    }
    private void CreateGraund() 
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

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


        for (int x = 0; x < _MAX_SIZE; x++)
        {
            for (int z = 0; z < _MAX_SIZE; z++)
            {
                int MapTypeNumber = int.Parse(csvDatas[x][z]);

                GameObject mapTile = GetRandomMapTile((MapTileType)MapTypeNumber);

                mapTile.transform.localPosition = new Vector3(x * MAP_RETO, 0, z * MAP_RETO);

                int randAngle = Random.Range(0, 4);

                mapTile.transform.eulerAngles = new Vector3(0, 90 * randAngle, 0);
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
                randomNumber = Random.Range(0, _flagAreaMap.mapTileMax);
                return GameObject.Instantiate(_flagAreaMap.GetMapTile(randomNumber));

            case MapTileType.Wall:
                randomNumber = Random.Range(0,_wallMap.mapTileMax);
                return GameObject.Instantiate(_wallMap.GetMapTile(randomNumber));

            case MapTileType.Nature:
                randomNumber = Random.Range(0, _natureMap.mapTileMax);
                return GameObject.Instantiate(_natureMap.GetMapTile(randomNumber));

        }

        return null;



    }

    private void CreateFlag() 
    {
        GameObject PlayerFlagObject = GameObject.Instantiate(_flagObjectBase);

        PlayerFlagObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = new Material[1] { _flagMaterial[0] };

        PlayerFlagObject.transform.position = _PLAYERFLAG_POSITION;

        GameObject enemyFlagObject = GameObject.Instantiate(_flagObjectBase);

        enemyFlagObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = new Material[1] { _flagMaterial[1] };

        enemyFlagObject.transform.position = _ENEMYFLAG_POSITION;





    }

    private void CreateMapWall()
    {
        int objectRate = 5;

        int objectspece = 2;


        for(int i = 0; i < objectRate * _MAX_SIZE; i++) 
        {
            GameObject wall = GameObject.Instantiate(_wallObject);
            Vector3 position = Vector3.zero;

            position.x = -MAP_RETO/2;
            position.y = 1.5f;
            position.z = i* objectspece-4;

            wall.transform.eulerAngles = new Vector3(0, 90, 0);

            wall.transform.position = position;


            wall = GameObject.Instantiate(_wallObject);
            position = Vector3.zero;

            position.x = MAP_RETO*_MAX_SIZE-MAP_RETO/2;
            position.y = 1.5f;
            position.z = i* objectspece-4;

            wall.transform.eulerAngles = new Vector3(0, 90, 0);

            wall.transform.position = position;

            wall = GameObject.Instantiate(_wallObject);
            position = Vector3.zero;

            position.x = i * objectspece - 4;
            position.y = 1.5f;
            position.z = MAP_RETO * _MAX_SIZE - MAP_RETO / 2;

            wall.transform.eulerAngles = new Vector3(0, 0, 0);

            wall.transform.position = position;


            wall = GameObject.Instantiate(_wallObject);
            position = Vector3.zero;

            position.x = i * objectspece - 4;
            position.y = 1.5f;
            position.z = -MAP_RETO / 2;

            wall.transform.eulerAngles = new Vector3(0, 0, 0);

            wall.transform.position = position;


        }





    }

}
