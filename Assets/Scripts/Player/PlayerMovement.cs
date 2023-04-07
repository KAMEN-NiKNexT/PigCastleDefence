using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        [Header("Movement Settigns")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        [Header("Movement Variables")]
        [SerializeField] private CharacterController _characterController;
        public Action OnMove;

        #endregion

        #region Conrol Methods

        public void MoveCharacter(Vector3 moveDirection)
        {
            OnMove?.Invoke();
            moveDirection *= _moveSpeed;
            moveDirection.y = MyGravity.Instance.GetGravityHandling(_characterController.isGrounded);
            _characterController.Move(moveDirection * Time.deltaTime);
        }
        public void RotateCharacter(Vector3 moveDirection)
        {
            if (_characterController.isGrounded)
            {
                if (Vector3.Angle(transform.forward, moveDirection) > 0)
                {
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                    transform.rotation = Quaternion.LookRotation(newDirection);
                }
            }
        }

        #endregion
    }
}