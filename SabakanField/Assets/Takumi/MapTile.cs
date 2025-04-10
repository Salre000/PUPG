using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapTile")]
public class MapTile : ScriptableObject
{

    //決め打ちの最大種類数
    const int _MAX_MAP_TILE = 10;

    //マップのオブジェクトリスト
    [SerializeField] private List<GameObject> _mapTile=new List<GameObject>(_MAX_MAP_TILE);

    //マップのオブジェクトを返す関数
    public GameObject GetMapTile(int number) { return _mapTile[number];}

    //このマップの種類数
    public int mapTileMax {  get { return _mapTile.Count; }}



}
