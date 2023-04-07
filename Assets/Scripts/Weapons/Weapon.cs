using System;
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
        [SerializeField] protected float _attackSpeed;
        protected float _attackTimer;
        public Action OnAttacked;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            StartCoroutine(UpdateTimer());
        }

        #endregion

        #region Control Methods

        public virtual void Attack() => OnAttacked?.Invoke();
        protected virtual IEnumerator UpdateTimer()
        {
            yield return new WaitForSeconds(_attackSpeed);
            _attackTimer = _attackSpeed;
        }

        #endregion
    }
}

