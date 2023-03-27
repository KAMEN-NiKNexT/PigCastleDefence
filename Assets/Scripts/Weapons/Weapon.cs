using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        #region Variables

        [Header("Weapon Settings")]
        [SerializeField] protected float _damage;

        #endregion

        #region Control Methods

        public abstract void Attack();

        #endregion
    }
}

