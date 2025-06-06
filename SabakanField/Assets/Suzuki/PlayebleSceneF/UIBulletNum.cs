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
            BulletManager.PlayerReload();
        }
    }

    private void BulletMagazineText()
    {
        _stringBuilder.Clear();
        _stringBuilder.Append(BulletManager.GetPlayerBulletMagazine());
        _bulletText.text = _stringBuilder.ToString();
        _stringBuilder.Clear();
        _stringBuilder.Append(BulletManager.GetBulletMagazine());
        _magazineText.text = _stringBuilder.ToString();
    }
}
