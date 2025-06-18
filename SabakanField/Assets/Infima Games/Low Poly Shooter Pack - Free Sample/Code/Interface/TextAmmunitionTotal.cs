// Copyright 2021, Infima Games. All Rights Reserved.

using System.Globalization;
using System.Text;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Total Ammunition Text.
    /// </summary>
    public class TextAmmunitionTotal : ElementText
    {
        private int ammunitionTotal = -1;
        bool start = false;
        #region METHODS
        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            //Total Ammunition.
            if (ammunitionTotal == -1)
            {
                // マガジンの総弾数
                ammunitionTotal = equippedWeapon.GetAmmunitionTotal()*3;

            }
            BulletManager.SetAllBulletMagazine(ammunitionTotal);
            //BulletManager.SetMAXMagazine(ammunitionTotal);

            Reload(ref ammunitionTotal);

            //Update Text.
            textMesh.text = ammunitionTotal.ToString(CultureInfo.InvariantCulture);

        }

        // マガジンの弾を補充した分引く
        private void Reload(ref int total)
        {
            if(!GameManager.Instance.IsReload()) return;
            BulletManager.ReloadSystem(total,equippedWeapon.GetAmmunitionCurrent(),equippedWeapon.GetAmmunitionAllTotal());
            GameManager.Instance.SetIsReload(false);
            total = BulletManager.GetMagazin();

        }


        
        #endregion
    }
}