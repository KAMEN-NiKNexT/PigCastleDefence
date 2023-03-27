using Kamen.Joystick;
using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PigCastleDefence.Weapons;

namespace PigCastleDefence.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyController : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private Weapon _weapon;

        #endregion

        #region Control Methods

        private void FixedUpdate()
        {
            if (!_enemyMovement.MoveToTarget())
            {
                _weapon.Attack();
            }
        }

        #endregion
    }
}