using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Player
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
            if (!_animator.GetBool("IsMove"))
            {
                _animator.SetBool("IsMove", true);
                _animator.SetBool("IsPigSpawn", false);
            }
        }
        protected override void OnAttackAnimation()
        {
            if (!_animator.GetBool("IsPigSpawn"))
            {
                _animator.SetBool("IsPigSpawn", true);
                _animator.SetBool("IsMove", false);
                _animator.SetTrigger("IsAttack");
            }
            else
            {
                _animator.SetTrigger("IsAttack");
            }
            //else _animator.SetTrigger("IsAttack");
        }

        #endregion
    }
}
