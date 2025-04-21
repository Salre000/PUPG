using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapTile", menuName = "ScriptableObjects/MapPlanData")]
//�}�b�v�̐�������CSV�ւ̃p�X�̖��O��Z�߂�X�v���N�^�u���I�u�W�F�N�g�N���X
public class MapPlanDataObject : ScriptableObject
{

    //�}�b�v�̃I�u�W�F�N�g���X�g
    [SerializeField] private List<string> _mapPlanDataName = new List<string>();

    //�}�b�v�v�����̖��O��Ԃ��֐�
    public string GetMapTileName(int number) { return _mapPlanDataName[number]; }

    //�}�b�v�f�[�^�̎�ސ�
    public int mapTileMax { get { return _mapPlanDataName.Count; } }



}