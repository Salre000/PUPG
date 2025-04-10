using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [Header("�ߐڐ퓬�̃}�b�v�I�u�W�F�N�g���X�g")]
    [SerializeField]private MapTile _CQBMap;
    [Header("�w�n�̋߂��̃}�b�v�I�u�W�F�N�g���X�g")]
    [SerializeField]private MapTile _frackAreaMap;
    [Header("�ǂ̑����̃}�b�v�I�u�W�F�N�g���X�g")]
    [SerializeField]private MapTile _wallMap;
    [Header("��Q���̏��Ȃ��̃}�b�v�I�u�W�F�N�g���X�g")]
    [SerializeField]private MapTile _natureMap;
    [Header("�}�b�v�̌��^�̃G�N�Z���f�[�^")]
    public TextAsset _mapPlanAseet;

    //  �}�b�v�̎�ނ̗񋓑�
    enum MapTileType 
    {
        CQB=0,
        Frack,
        Wall,
        Nature
    }

    public void Awake()
    {
        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //for���p�B��s�ڂ͓ǂݍ��܂Ȃ�
        int i = 1;

        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(_mapPlanAseet.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
        }

        int ss = 0;

    }



}
