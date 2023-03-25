using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.Joystick;

namespace PigCastleDefence.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerCombat))]
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCombat _playerCombat;

        private Vector3 _joystickValue;

        #endregion

        #region Control Methods

        private void Update()
        {
            _joystickValue = _joystick.GetVectorDirection();

            if (_joystickValue.x != 0 || _joystickValue.y != 0)
            {
                _playerMovement.MoveCharacter(_joystickValue);
                _playerMovement.RotateCharacter(_joystickValue);
            }
            else
            {
                _playerCombat.SpawnPig();
            }
        }

        #endregion
    }
}