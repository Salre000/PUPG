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

    [SerializeField]
    private Transform _targetPos;

    const float _num = 80f;
    float _plusVecY = 0f;
    float _plusVecX = 2500;
    const int _PLAYER_NUM = 10;

    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        for(int i=0;i < _PLAYER_NUM; i++)
        {
            GameObject instanObject = Instantiate(_playerResultsPanel, _parent.transform);
            Vector3 position = instanObject.transform.position;
            position.y += _plusVecY;
            position.x -= _plusVecX;
            instanObject.transform.position = position;
            _objectList.Add(instanObject);

            _plusVecY -= _num;
            _plusVecX += 500f;
        }
        _plusVecY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _objectList.Count; i++)
        {
            Vector3 position = _objectList[i].transform.localPosition;
            position.x = Mathf.Lerp(_objectList[i].transform.localPosition.x, _targetPos.localPosition.x, Time.deltaTime*speed);
            _objectList[i].transform.localPosition = position;
        }
    }
}
