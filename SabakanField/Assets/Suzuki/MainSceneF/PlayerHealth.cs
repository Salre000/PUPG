using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _hp = 0;
    [SerializeField]
    private TextMeshProUGUI _text;
    private StringBuilder stringBuilder=new();

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Initialize();

    }

    private void Initialize()
    {
        // 0==ÉvÉåÉCÉÑÅ[ÇÃHP
        _hp = (int)ChracterHPManager.instance.GetHp(0);
        if (_hp < 0) _hp = 0;
        stringBuilder.Clear();
        stringBuilder.Append(_hp);
        _text.text = stringBuilder.ToString();
    }
}
