using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class PlayerAnimatorController : AnimatorController
    {
        #region Variables

        [SerializeField] private PlayerMovement _playerMovement;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            base.Start();
            _playerMovement.OnMove += OnMoveAnimation;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _playerMovement.OnMove -= OnMoveAnimation;
        }

        #endregion

        #region Control Methods

        private void OnMoveAnimation()
        {
            if (_animator.GetBool("IsPigSpawn")) _animator.SetBool("IsPigSpawn", false);
        }
        protected override void OnAttackAnimation()
        {
            if (!_animator.GetBool("IsPigSpawn")) _animator.SetBool("IsPigSpawn", true);
            else _animator.SetTrigger("IsAttack");
        }

        #endregion
    }
}
