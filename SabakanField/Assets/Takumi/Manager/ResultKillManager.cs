using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-100)]
public class ResultKillManager : MonoBehaviour
{
    public static ResultKillManager initialize;

    private readonly Color ENEMYCOLOR = Color.red;
    private readonly Color PLAYERCOLOR = Color.blue;

    public void Awake()
    {
        count = AIManager.kIll;

        initialize = this;


    }

    KIllCount count;

    public void SetStatus(GameObject model, int index,int BScore)
    {
        model.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =index==0?"Player" :count.Name[index-1].ToString();

        model.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = BScore.ToString();

        model.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = count.killCount[index].ToString();
        model.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = count.deathCount[index].ToString();
        model.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = count.assistCount[index].ToString();

        model.GetComponent<Image>().color = count.players[index] ? PLAYERCOLOR : ENEMYCOLOR;

    }



}
