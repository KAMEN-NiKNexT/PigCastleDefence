using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class ManaPotion : Item
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private int _manaRestoreAmount;

        #endregion

        #region Control Methods

        public override void Use()
        {
            if (_owner.TryGetComponent(out IManaUser manaUser)) manaUser.RestoreMana(_manaRestoreAmount);
            //TODO: Add effect for restore mana
        }

        #endregion
    }
}