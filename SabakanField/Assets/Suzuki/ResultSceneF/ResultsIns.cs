using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResultsIns : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerResultsPanel;
    [SerializeField]
    private GameObject _parent;

    private List<GameObject> _objectList = new(_PLAYER_NUM);
    private List<int> _battelScoreSortList = new(_PLAYER_NUM);
    private List<int> _battelScoreIDList = new(_PLAYER_NUM);

    [SerializeField]
    private Transform _targetPos;

    const float _num = 80f;
    float _plusVecY = 0f;
    float _plusVecX = 2500;
    const int _PLAYER_NUM = 10;

    private float _speed = 0.01f;
    private float _time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = 120;
        count = AIManager.kIll;
        for (int i = 0; i < _PLAYER_NUM; i++)
            _battelScoreIDList.Add(i);
        BattelScoreSort();
        InstantiateScorePanel();
    }
    KIllCount count;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _objectList.Count; i++)
        {
            _time += Time.deltaTime * _speed;
            Vector3 position = _objectList[i].transform.localPosition;
            position.x = Mathf.Lerp(_objectList[i].transform.localPosition.x, _targetPos.localPosition.x, _time);
            _objectList[i].transform.localPosition = position;
        }
    }

    private void BattelScoreSort()
    {
        for (int i = 0; i < _PLAYER_NUM; i++)
        {
            int BScore = (int)((count.killCount[i] * 100 + count.assistCount[i] * 15) + count.deathCount[i] * 5);
            _battelScoreSortList.Add(BScore);
        }
        int max = 0;
        int index = 0;
        // 降順ソート
        for (int j = 0; j < _PLAYER_NUM; j++)
        {
            index = j;
            max = 0;
            for (int i = index; i < _PLAYER_NUM; i++)
            {
                if (max < _battelScoreSortList[i])
                {
                    // 今の最大値を代入
                    max = _battelScoreSortList[i];
                    // 一番上と入れ替える
                    _battelScoreSortList[i] = _battelScoreSortList[index];
                    _battelScoreSortList[index] = max;
                    // IDも一緒に入れ替える
                    int num = _battelScoreIDList[i];
                    _battelScoreIDList[i] = _battelScoreIDList[index];
                    _battelScoreIDList[index] = num;
                }
            }

        }
    }

    private void InstantiateScorePanel()
    {
        for (int i = 0; i < _PLAYER_NUM; i++)
        {
            GameObject instanObject = Instantiate(_playerResultsPanel, _parent.transform);
            Vector3 position = instanObject.transform.position;
            position.y += _plusVecY;
            position.x -= _plusVecX;
            // 奇数は右から登場させる
            if (i % 2 == 1)
                position.x *= -2;
            instanObject.transform.position = position;

            ResultKillManager.initialize.SetStatus(instanObject, _battelScoreIDList[i], _battelScoreSortList[i]);

            _objectList.Add(instanObject);

            _plusVecY -= _num;
            _plusVecX += 500f;
        }
    }
}
