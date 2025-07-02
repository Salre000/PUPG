using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[DefaultExecutionOrder(-100)]
public class ResultKillManager : MonoBehaviour
{
    public static ResultKillManager initialize;

    public void Awake()
    {
        count = AIManager.kIll;

        initialize = this;


    }

    KIllCount count;

    public void SetStatus(GameObject model, int index)
    {
        model.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count.Name[index].ToString();

        model.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = count.killCount[index].ToString();
        model.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = count.deathCount[index].ToString();
        model.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = count.assistCount[index].ToString();



    }



}
