using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    List<Image> color = new List<Image>();
    List<TextMeshProUGUI> Name = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> Score = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> kill = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> death = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> assert = new List<TextMeshProUGUI>();

    public void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Name.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            Name[i].text = "NULL";
            Score.Add(transform.GetChild(i).transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            kill.Add(transform.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>());
            death.Add(transform.GetChild(i).transform.GetChild(3).GetComponent<TextMeshProUGUI>());
            assert.Add(transform.GetChild(i).transform.GetChild(4).GetComponent<TextMeshProUGUI>());
        }

    }


    private void FixedUpdate()
    {
        KIllCount kIllCount = AIManager.kIll;
        if (kIllCount == null) return;

        for (int i = 0; i < transform.childCount; i++)
        {

            Score[i].text =((int)((kIllCount.killCount[i] * 100 + kIllCount.assistCount[i] * 15) + kIllCount.deathCount[i] * 5)).ToString();
            kill[i].text = kIllCount.killCount[i].ToString();
            death[i].text = kIllCount.deathCount[i].ToString();
            assert[i].text = kIllCount.assistCount[i].ToString();


            if (Name[i].text != "NULL") continue;
            Name[i].text = i==0?"Player":kIllCount.Name[i-1];


        }

    }
}
