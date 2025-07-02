using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabChecks : MonoBehaviour
{
    [SerializeField] private GameObject _playerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            _playerCanvas.SetActive(true);
        }
        else
        {
            _playerCanvas.SetActive(false);

        }
    }
}
