using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapTile")]
public class MapTile : ScriptableObject
{

    //���ߑł��̍ő��ސ�
    const int _MAX_MAP_TILE = 10;

    //�}�b�v�̃I�u�W�F�N�g���X�g
    [SerializeField] private List<GameObject> _mapTile=new List<GameObject>(_MAX_MAP_TILE);

    //�}�b�v�̃I�u�W�F�N�g��Ԃ��֐�
    public GameObject GetMapTile(int number) { return _mapTile[number];}

    //���̃}�b�v�̎�ސ�
    public int mapTileMax {  get { return _mapTile.Count; }}



}
