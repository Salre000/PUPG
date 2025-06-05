using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
//�}�b�v�̐������s���N���X
public class CreateMap : MonoBehaviour
{
    [SerializeField, Header("�ߐڐ퓬�̃}�b�v�I�u�W�F�N�g���X�g")] private MapTile _CQBMap;
    [SerializeField, Header("�w�n�̋߂��̃}�b�v�I�u�W�F�N�g���X�g")] private MapTile _flagAreaMap;
    [SerializeField, Header("�ǂ̑����̃}�b�v�I�u�W�F�N�g���X�g")] private MapTile _wallMap;
    [SerializeField, Header("��Q���̏��Ȃ��̃}�b�v�I�u�W�F�N�g���X�g")] private MapTile _natureMap;
    [SerializeField, Header("�e����[�ł���}�b�v�I�u�W�F�N�g���X�g")] private MapTile _amoReChageMap;
    [SerializeField, Header("���킪�؂�ւ�����}�b�v�I�u�W�F�N�g���X�g")] private MapTile _WeaponMap;
    [SerializeField, Header("�}�b�v�̌��^�̃G�N�Z���f�[�^�̖��O�������Ă���I�u�W�F�N�g")] private MapPlanDataObject _planData;
    [SerializeField, Header("�}�b�v�ɔz�u����t���b�O�̃��f���I�u�W�F�N�g")] private GameObject _flagObjectBase;
    [SerializeField, Header("�t���b�O�̃}�e���A���A�v���C���[�A�G�l�~�[�̏���")] private Material[] _flagMaterial = new Material[2];
    [SerializeField, Header("�}�b�v�͈̔͊O�Ƌ�؂�ǂ̃v���n�u")] private GameObject _wallObject;
    [SerializeField, Header("AI�̃v���n�u�x�[�X")] private GameObject _aiObject;

    //�}�b�v�̐����̃f�[�^�̎Q�Ɛ�̃p�X
    private readonly string _PLAN_PASS = "MapPlanData/";

    //�G�܂��͖����̃t���b�N�I�u�W�F�N�g��Ԃ�
    public GameObject GetFlag(int number) { return flag[number]; }
    //�t���b�N�̃I�u�W�F�N�g�z��
    GameObject[] flag = new GameObject[2];

    //�}�b�v�̈�ӂ̃}�b�v�`�b�v��
    [SerializeField] int _MAX_SIZE_X = 6;
    [SerializeField] int _MAX_SIZE_Y = 6;

    public int MAXSIZE = -1;

    public Vector2 MapSize=Vector2.zero;

    //�}�b�v�̍���
    const float _MAP_HEIGHT = 0.2f;

    //�}�b�v�`�b�v�̃T�C�Y
    const int MAP_RETO = 10;

    //�G�Ɩ����̃t���b�O�̍��W
    private Vector3 _ENEMYFLAG_POSITION;
    private Vector3 _PLAYERFLAG_POSITION;
    
    //  �}�b�v�̎�ނ̗񋓑�
    enum MapTileType
    {
        CQB = 0,
        Frack,
        Wall,
        Nature,
        AmmunitionReCharge,//�e��⋋�ł���ꏊ
        WeaponSpawn//���킪�X�|�[������ꏊ
    }

    private AIManager _AIManager;

    public void Awake()
    {
        MAXSIZE = Mathf.Min(_MAX_SIZE_X, _MAX_SIZE_Y);
        MapSize.x = _MAX_SIZE_X;
        MapSize.y = _MAX_SIZE_Y;

        _ENEMYFLAG_POSITION = new Vector3((MAP_RETO * _MAX_SIZE_X) - (MAP_RETO + (MAP_RETO / 2)), _MAP_HEIGHT, (MAP_RETO * _MAX_SIZE_Y) - (MAP_RETO + (MAP_RETO / 2)));
        _PLAYERFLAG_POSITION = new Vector3((MAP_RETO / 2), _MAP_HEIGHT, (MAP_RETO / 2));

        //  �}�b�v�̎�ނ̗񋓑�

        AIUtility.aIManager = _AIManager = GetComponent<AIManager>();

        CreateMapManager.createMap = this;

        //�n�ʂƏ�Q���𐶐�����֐�
        CreateGraund();

        //�t���b�O�̐�������֐�
        CreateFlag();

        //�}�b�v�̋��ڂ̕ǂ𐶐�����֐�
        CreateMapWall();

        //Ai�𐶐�����֐�
        _AIManager.CreateAI();

    }

    public void Start()
    {
    }
    private void CreateGraund()
    {
        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(_PLAN_PASS);
        builder.Append(_planData.GetMapTileName((int)MapTypeEnum.MapType._normalMAP));


        //�q�����t�@�C���p�X���g���t�@�C���̃��[�h���s��
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
        }

        //�}�b�v��1�̃I�u�W�F�N�g�̎q�\���ɂ��邽�߂̃I�u�W�F�N�g
        GameObject maptileAll = new GameObject("MapTileAll");

        //int�z��ɕϊ������}�b�v�f�[�^���g���}�b�v�`�b�v�����蓖�Ă���
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

    //�񋓑̂ɂ�������ނ̃}�b�v�`�b�v�������_���ɐ���
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
    //�t���b�O�𐶐����č��W�ƃJ���[��ύX����֐�
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

    //�}�b�v�̊O�����͂ނ悤�ɕǂ𐶐�����֐�
    private void CreateMapWall()
    {
        int objectRate = 5;

        int objectspece = 2;

        GameObject wallAll = new GameObject();

        //�ǂ�1�̃I�u�W�F�N�g�̎q�\���ɂ��邽�߂̃I�u�W�F�N�g
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

        //��ӂ̒����ƕǈꖇ�̒������g���ǂ���ʓ\�郋�[�v
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
