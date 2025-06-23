using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectAnim : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        GetComponent<Button>().onClick.AddListener(OnButton);

    }

    private void Update()
    {
        Animation();
    }

    void OnButton()
    {
        Debug.Log("n");
    }

    private void OnMouseEnter()
    {
        SelectManager.Instance.SetFlagModeSelect(true);
    }

    private void OnMouseExit()
    {
        SelectManager.Instance.SetFlagModeSelect(false);
    }

    private void Animation()
    {
        bool value = SelectManager.Instance.GetFlagModeSelect();
        Debug.Log(value);

    }
}
