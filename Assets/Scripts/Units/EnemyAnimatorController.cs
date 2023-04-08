using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Enemy
{
    public class EnemyAnimatorController : AnimatorController
    {
        #region Variables

        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private Phantom _phantom;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            base.Start();
            _enemyMovement.OnMove += OnMoveAnimation;
            //TODO: Change phantom object and add GUI script
            if (_phantom != null) _phantom.OnDodged += OnUseAbilityAnimation;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _enemyMovement.OnMove -= OnMoveAnimation;
            if (_phantom != null) _phantom.OnDodged -= OnUseAbilityAnimation;
        }

        #endregion

        #region Control Methods

        private void OnMoveAnimation()
        {
            if (_animator.GetBool("IsReachedToTarget")) _animator.SetBool("IsReachedToTarget", false);
        }
        protected override void OnAttackAnimation()
        {
            if (!_animator.GetBool("IsReachedToTarget")) _animator.SetBool("IsReachedToTarget", true);
            else _animator.SetTrigger("IsAttack");
        }
        private void OnUseAbilityAnimation() => _animator.SetTrigger("IsAbility");

        #endregion
    }
}