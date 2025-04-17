using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
public class TextResultTest : MonoBehaviour
{
    TextMeshProUGUI This;

    private readonly int KILL_COUNT =0;
    private readonly int DEATH_COUNT =1;
    private readonly int FRIENDLY_FIRE_COUNT =2;
    private readonly int COUNT_TIME = 3;
    // Start is called before the first frame update
    void Start()
    {
        This=GetComponent<TextMeshProUGUI>();

        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        TextAsset textAsset = Resources.Load<TextAsset>("InGameData/ SaveData");

        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
        }


        This.text = csvDatas[0][0];




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
