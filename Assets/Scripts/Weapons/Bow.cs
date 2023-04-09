using PigCastleDefence.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class Bow : Weapon
    {
        #region Variables

        [Header("Bow Settings")]
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private float _arrowSpeed;
        [SerializeField] private float _arrowGravity;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targetMask;

        #endregion

        #region Control Methods

        public override void Attack()
        {
            if (_attackTimer >= _attackSpeed)
            {
                // TODO: Fix this script in the future
                base.Attack();
                GameObject arrowObject = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity);
                Arrow arrow = arrowObject.GetComponent<Arrow>();
                Vector3 direction = transform.rotation * Vector3.forward;
                arrow.Shoot(direction, _arrowSpeed, _damage);
                _attackTimer = 0;
                StartCoroutine(UpdateTimer());
            }
        }

        #endregion
    }
}