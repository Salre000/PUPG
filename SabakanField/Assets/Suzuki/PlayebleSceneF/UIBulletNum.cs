using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIBulletNum : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bulletText;
    [SerializeField]
    private TextMeshProUGUI _magazineText;

    private StringBuilder _stringBuilder=new StringBuilder();

    private void Update()
    {
        BulletMagazineText();

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerManager.PlayerReload();
        }
    }

    private void BulletMagazineText()
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(PlayerManager.GetPlayerBulletMagazine());
        _bulletText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _stringBuilder.Append(PlayerManager.GetBulletMagazine());
        _magazineText.text = _stringBuilder.ToString();
    }
}
