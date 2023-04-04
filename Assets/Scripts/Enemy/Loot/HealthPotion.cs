using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class HealthPotion : Item
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private int _healthRestoreAmount;

        #endregion

        #region Control Methods

        public override void Use()
        {
            _owner.Heal(_healthRestoreAmount);
            //TODO: Add effect for restore health
        }

        #endregion
    }
}