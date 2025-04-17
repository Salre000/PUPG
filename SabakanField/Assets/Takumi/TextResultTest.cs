using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
public class TextResultTest : MonoBehaviour
{
    TextMeshProUGUI This;

    private readonly int KILL_COUNT =0;
    private readonly int DEATH_COUNT =1;
    private readonly int FRIENDLY_FIRE_COUNT =2;
    private readonly int COUNT_TIME = 3;
    // Start is called before the first frame update
    void Start()
    {
        This=GetComponent<TextMeshProUGUI>();

        //“Ç‚İ‚ñ‚¾CSVƒtƒ@ƒCƒ‹‚ğŠi”[
        List<string[]> csvDatas = new List<string[]>();

        //CSVƒtƒ@ƒCƒ‹‚Ìs”‚ğŠi”[
        int height = 0;

        TextAsset textAsset = Resources.Load<TextAsset>("InGameData/ SaveData");

        //“Ç‚İ‚ñ‚¾ƒeƒLƒXƒg‚ğStringŒ^‚É‚µ‚ÄŠi”[
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,‚Å‹æØ‚Á‚ÄCSV‚ÉŠi”[
            csvDatas.Add(line.Split(','));
            height++; // s”‰ÁZ
        }


        This.text = csvDatas[0][0];




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
