// Copyright 2021, Infima Games. All Rights Reserved.

using System.Collections;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Total Ammunition Text.
    /// </summary>
    public class TextAmmunitionTotal : ElementText
    {
        private int ammunitionTotal = -1;
        bool start = false;
        private int _total = 0;
        #region METHODS
        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            //Total Ammunition.
            //if (ammunitionTotal == -1)
            //{
            //    // マガジンの総弾数
            //    ammunitionTotal = equippedWeapon.GetAmmunitionTotal()*3;

            //}
            //BulletManager.SetAllBulletMagazine(ammunitionTotal);


            //Update Text.
            textMesh.text = BulletManager.GetMagazin().ToString();/*ammunitionTotal.ToString(CultureInfo.InvariantCulture);*/
        }

        


        #endregion
    }
}