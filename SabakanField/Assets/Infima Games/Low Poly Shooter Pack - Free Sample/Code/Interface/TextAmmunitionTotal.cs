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
        #region METHODS
        /// <summary>
        /// Tick.
        /// </summary>
        protected override void Tick()
        {
            //Update Text.
            textMesh.text = BulletManager.GetMagazin().ToString();
        }

        


        #endregion
    }
}