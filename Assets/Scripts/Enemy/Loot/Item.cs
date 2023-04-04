using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public abstract class Item : MonoBehaviour
    {
        #region Variables

        protected Unit _owner;

        #endregion

        #region Control Methods

        public void SetOwner(Unit owner) => _owner = owner;
        public abstract void Use();

        #endregion
    }
}