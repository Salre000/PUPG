using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSolo : MonoBehaviour
{
    [SerializeField] GameObject modeselectPanel;
    // Start is called before the first frame update
    void Awake()
    {
        modeselectPanel.SetActive(false);
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnStart());
    }

    void OnStart()
    {
        modeselectPanel.SetActive(true);
        gameObject.SetActive(false );
    }
    
}
