using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
//マップの生成を行うクラス
public class CreateMap : MonoBehaviour
{
    [SerializeField, Header("近接戦闘のマップオブジェクトリスト")] private MapTile _CQBMap;
    [SerializeField, Header("陣地の近くのマップオブジェクトリスト")] private MapTile _flagAreaMap;
    [SerializeField, Header("壁の多いのマップオブジェクトリスト")] private MapTile _wallMap;
    [SerializeField, Header("障害物の少ないのマップオブジェクトリスト")] private MapTile _natureMap;
    [SerializeField, Header("弾が補充できるマップオブジェクトリスト")] private MapTile _amoReChageMap;
    [SerializeField, Header("武器が切り替えられるマップオブジェクトリスト")] private MapTile _WeaponMap;
    [SerializeField, Header("マップの原型のエクセルデータの名前が入っているオブジェクト")] private MapPlanDataObject _planData;
    [SerializeField, Header("マップに配置するフラッグのモデルオブジェクト")] private GameObject _flagObjectBase;
    [SerializeField, Header("フラッグのマテリアル、プレイヤー、エネミーの順番")] private Material[] _flagMaterial = new Material[2];
    [SerializeField, Header("マップの範囲外と区切る壁のプレハブ")] private GameObject _wallObject;
    [SerializeField, Header("AIのプレハブベース")] private GameObject _aiObject;

    //マップの生成のデータの参照先のパス
    private readonly string _PLAN_PASS = "MapPlanData/";

    //敵または味方のフラックオブジェクトを返す
    public GameObject GetFlag(int number) { return flag[number]; }
    //フラックのオブジェクト配列
    GameObject[] flag = new GameObject[2];

    //マップの一辺のマップチップ数
    [SerializeField] int _MAX_SIZE_X = 6;
    [SerializeField] int _MAX_SIZE_Y = 6;

    public int MAXSIZE = -1;

    public Vector2 MapSize=Vector2.zero;

    //マップの高さ
    const float _MAP_HEIGHT = 0.2f;

    //マップチップのサイズ
    const int MAP_RETO = 10;

    //敵と味方のフラッグの座標
    private Vector3 _ENEMYFLAG_POSITION;
    private Vector3 _PLAYERFLAG_POSITION;
    
    //  マップの種類の列挙体
    enum MapTileType
    {
        CQB = 0,
        Frack,
        Wall,
        Nature,
        AmmunitionReCharge,//弾を補給できる場所
        WeaponSpawn//武器がスポーンする場所
    }

    private AIManager _AIManager;

    public void Awake()
    {
        MAXSIZE = Mathf.Min(_MAX_SIZE_X, _MAX_SIZE_Y);
        MapSize.x = _MAX_SIZE_X;
        MapSize.y = _MAX_SIZE_Y;

        _ENEMYFLAG_POSITION = new Vector3((MAP_RETO * _MAX_SIZE_X) - (MAP_RETO + (MAP_RETO / 2)), _MAP_HEIGHT, (MAP_RETO * _MAX_SIZE_Y) - (MAP_RETO + (MAP_RETO / 2)));
        _PLAYERFLAG_POSITION = new Vector3((MAP_RETO / 2), _MAP_HEIGHT, (MAP_RETO / 2));

        //  マップの種類の列挙体

        AIUtility.aIManager = _AIManager = GetComponent<AIManager>();

        CreateMapManager.createMap = this;

        //地面と障害物を生成する関数
        CreateGraund();

        //フラッグの生成する関数
        CreateFlag();

        //マップの境目の壁を生成する関数
        CreateMapWall();

        //Aiを生成する関数
        _AIManager.CreateAI();

    }

    public void Start()
    {
    }
    private void CreateGraund()
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(_PLAN_PASS);
        builder.Append(_planData.GetMapTileName((int)MapTypeEnum.MapType._normalMAP));


        //繋げたファイルパスを使いファイルのロードを行う
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

        //マップを1つのオブジェクトの子構造にするためのオブジェクト
        GameObject maptileAll = new GameObject("MapTileAll");

        //int配列に変換したマップデータを使いマップチップを割り当てする
        for (int x = 0; x < _MAX_SIZE_X; x++)
        {
            for (int z = 0; z < _MAX_SIZE_Y; z++)
            {

                int MapTypeNumber = int.Parse(csvDatas[x][z]);

                GameObject mapTile = GetRandomMapTile((MapTileType)MapTypeNumber);

                mapTile.transform.parent = maptileAll.transform;

                mapTile.transform.localPosition = new Vector3(x * MAP_RETO, 0, z * MAP_RETO);

                int randAngle = Random.Range(0, 4);

                mapTile.transform.eulerAngles = new Vector3(0, 90 * randAngle, 0);

            }
        }



    }

    //列挙体にあった種類のマップチップをランダムに生成
    private GameObject GetRandomMapTile(MapTileType mapType)
    {

        int randomNumber = -1;

        switch (mapType)
        {
            case MapTileType.CQB:
                randomNumber = Random.Range(0, _CQBMap.mapTileMax);
                return GameObject.Instantiate(_CQBMap.GetMapTile(randomNumber));

            case MapTileType.Frack:
                randomNumber = Random.Range(0, _flagAreaMap.mapTileMax);
                return GameObject.Instantiate(_flagAreaMap.GetMapTile(randomNumber));

            case MapTileType.Wall:
                randomNumber = Random.Range(0, _wallMap.mapTileMax);
                return GameObject.Instantiate(_wallMap.GetMapTile(randomNumber));

            case MapTileType.Nature:
                randomNumber = Random.Range(0, _natureMap.mapTileMax);
                return GameObject.Instantiate(_natureMap.GetMapTile(randomNumber));
            case MapTileType.AmmunitionReCharge:

                randomNumber = Random.Range(0, _amoReChageMap.mapTileMax);
                return GameObject.Instantiate(_amoReChageMap.GetMapTile(randomNumber));

            case MapTileType.WeaponSpawn:

                randomNumber = Random.Range(0, _WeaponMap.mapTileMax);
                return GameObject.Instantiate(_WeaponMap.GetMapTile(randomNumber));
        }

        return null;



    }
    //フラッグを生成して座標とカラーを変更する関数
    private void CreateFlag()
    {
        flag[0] = GameObject.Instantiate(_flagObjectBase);

        flag[0].transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = new Material[1] { _flagMaterial[0] };

        flag[0].transform.position = _PLAYERFLAG_POSITION;

        _AIManager.SetFlagObject(flag[0], 0);

        flag[1] = GameObject.Instantiate(_flagObjectBase);

        flag[1].transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = new Material[1] { _flagMaterial[1] };

        flag[1].transform.position = _ENEMYFLAG_POSITION;
        _AIManager.SetFlagObject(flag[1], 1);

    }

    //マップの外周を囲むように壁を生成する関数
    private void CreateMapWall()
    {
        int objectRate = 5;

        int objectspece = 2;

        GameObject wallAll = new GameObject();

        //壁を1つのオブジェクトの子構造にするためのオブジェクト
        wallAll.transform.name = "WallAll";
        for (int i = 0; i < objectRate * _MAX_SIZE_Y; i++)
        {
            GameObject wall = GameObject.Instantiate(_wallObject, wallAll.transform);
            Vector3 position = Vector3.zero;

            position.x = -MAP_RETO / 2;
            position.y = 1.5f;
            position.z = i * objectspece - 4;

            wall.transform.eulerAngles = new Vector3(0, 90, 0);

            wall.transform.position = position;


            wall = GameObject.Instantiate(_wallObject, wallAll.transform);
            position = Vector3.zero;

            position.x = MAP_RETO * _MAX_SIZE_X - MAP_RETO / 2;
            position.y = 1.5f;
            position.z = i * objectspece - 4;

            wall.transform.eulerAngles = new Vector3(0, 90, 0);

            wall.transform.position = position;


        }

        //一辺の長さと壁一枚の長さを使い壁を一面貼るループ
        for (int i = 0; i < objectRate * _MAX_SIZE_X; i++)
        {
            GameObject wall = GameObject.Instantiate(_wallObject, wallAll.transform);

            Vector3 position = Vector3.zero;

            wall = GameObject.Instantiate(_wallObject, wallAll.transform);
            position = Vector3.zero;

            position.x = i * objectspece - 4;
            position.y = 1.5f;
            position.z = -MAP_RETO / 2;

            wall.transform.eulerAngles = new Vector3(0, 0, 0);

            wall.transform.position = position;
            wall = GameObject.Instantiate(_wallObject, wallAll.transform);
            position = Vector3.zero;

            position.x = i * objectspece - 4;
            position.y = 1.5f;
            position.z = MAP_RETO * _MAX_SIZE_Y - MAP_RETO / 2;

            wall.transform.eulerAngles = new Vector3(0, 0, 0);

            wall.transform.position = position;


        }
    }
}
