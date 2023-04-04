using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PigCastleDefence
{
    public class Coin : Item
    {
        #region Variables

        [SerializeField] private int _amount;

        #endregion

        #region Control Methods

        public override void Use()
        {
            //TODO: Get coin
        }

        #endregion
    }

}