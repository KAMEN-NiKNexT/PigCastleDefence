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
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targetMask;
        private float _attackTimer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _attackTimer = _attackSpeed;
        }

        private void Update()
        {
            if (_attackTimer < _attackSpeed) _attackTimer += Time.deltaTime;
        }

        #endregion

        #region Control Methods

        public override void Attack()
        {
            if (_attackTimer >= _attackSpeed)
            {
                // TODO: Fix this script in the future
                GameObject arrowObject = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity);
                Arrow arrow = arrowObject.GetComponent<Arrow>();
                arrow.SetDamage(_damage);
                arrow.SetSpeed(_arrowSpeed);
                Vector3 direction = transform.rotation * Vector3.forward;
                arrow.SetDirection(direction);
                arrow.Shoot();
                _attackTimer = 0;
            }
        }

        #endregion
    }
}