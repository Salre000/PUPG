using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : MonoBehaviour
{
    Camera camera;
    bool oneFlag = false;

    private void Awake()
    {
        camera=GetComponent<Camera>();
    }
    private void OnEnable()
    {
        for (int i = 0; i < AICharacterUtility.CharacterCount(); i++)
            AICharacterUtility.ChengeOutLine(i, false);
        oneFlag = false;

    }
    void OnDisable ()
    {
        for (int i = 0; i < AICharacterUtility.CharacterCount(); i++)
            AICharacterUtility.ChengeOutLine(i, false);
        oneFlag = false;

    }

    private void FixedUpdate()
    {
        Debug.Log(Camera.main.name+"ƒƒCƒ“ƒJƒƒ‰");
        List<AI> AIS=AICharacterUtility.GetAIS();
        for(int i=0;i< AIS.Count; i++) 
        {


            var pos = camera.WorldToScreenPoint(AIS[i].transform.position);

            if (pos.x < 0 || pos.x > 1920) continue;
            if (pos.y < 0 || pos.y > 1080) continue;

            AICharacterUtility.ChengeOutLine(i, true);





        }

        if (oneFlag) return;
        for (int i = 0; i < AICharacterUtility.CharacterCount(); i++)
            AICharacterUtility.ChengeOutLine(i, false);
        oneFlag = true;

    }



}
