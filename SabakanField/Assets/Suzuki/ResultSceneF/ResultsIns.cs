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

    private List<GameObject>_objectList=new(_PLAYER_NUM);
    private List<int>_battelScoreSortList=new(_PLAYER_NUM);

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
        count = AIManager.kIll;
        Application.targetFrameRate = 120;
        BattelScoreSort();
        InstantiateScorePanel();
    }
    KIllCount count;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _objectList.Count; i++)
        {
            _time+=Time.deltaTime*_speed;
            Vector3 position = _objectList[i].transform.localPosition;
            position.x = Mathf.Lerp(_objectList[i].transform.localPosition.x, _targetPos.localPosition.x, _time);
            _objectList[i].transform.localPosition = position;
        }
    }

    private void BattelScoreSort()
    {
        for (int i = 0; i < _PLAYER_NUM; i++)
        {
            int BScore = UnityEngine.Random.Range(0, 1000);//(int)((count.killCount[i] * 100 + count.assistCount[i] * 15) + count.deathCount[i] * 5);
            _battelScoreSortList.Add(BScore);
        }
        // ¸‡
        _battelScoreSortList.Sort();
    }

    private void InstantiateScorePanel()
    {
        for (int i = 0; i < _PLAYER_NUM; i++)
        {
            GameObject instanObject = Instantiate(_playerResultsPanel, _parent.transform);
            Vector3 position = instanObject.transform.position;
            position.y += _plusVecY;
            position.x -= _plusVecX;
            // Šï”‚Í‰E‚©‚ç“oê‚³‚¹‚é
            if (i % 2 == 1)
                position.x *= -2;
            instanObject.transform.position = position;

            ResultKillManager.initialize.SetStatus(instanObject, _battelScoreSortList[i]);

            _objectList.Add(instanObject);

            _plusVecY -= _num;
            _plusVecX += 500f;
        }
    }
}
