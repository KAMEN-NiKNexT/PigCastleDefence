using PigCastleDefence.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class EnemyAnimatorController : AnimatorController
    {
        #region Variables

        [SerializeField] private EnemyMovement _enemyMovement;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            base.Start();
            _enemyMovement.OnMove += OnMoveAnimation;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _enemyMovement.OnMove -= OnMoveAnimation;
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

        #endregion
    }
}