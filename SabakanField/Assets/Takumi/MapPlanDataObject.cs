using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapPlanData")]
//マップの生成情報のCSVへのパスの名前を纏めるスプリクタブルオブジェクトクラス
public class MapPlanDataObject : ScriptableObject
{

    //マップのオブジェクトリスト
    [SerializeField] private List<string> _mapPlanDataName = new List<string>();

    //マッププランの名前を返す関数
    public string GetMapTileName(int number) { return _mapPlanDataName[number]; }

    //マップデータの種類数
    public int mapTileMax { get { return _mapPlanDataName.Count; } }



}