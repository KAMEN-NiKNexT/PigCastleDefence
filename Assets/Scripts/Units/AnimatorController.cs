using PigCastleDefence.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public abstract class AnimatorController : MonoBehaviour
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected Animator _animator;

        [Header("Objects")]
        [SerializeField] protected Unit _unit;
        [SerializeField] protected Weapon _weapon;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            _unit.OnUnitDied += OnDiedAnimation;
            _weapon.OnAttacked += OnAttackAnimation;
        }
        protected virtual void OnDestroy()
        {
            _unit.OnUnitDied -= OnDiedAnimation;
            _weapon.OnAttacked -= OnAttackAnimation;
        }

        #endregion

        #region Control Methods

        private void OnDiedAnimation(Unit owner) => _animator.SetTrigger("IsDied");
        protected abstract void OnAttackAnimation();

        #endregion
    }
}