using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class Fists : Weapon
    {
        #region Variables

        [Header("Fists Settigns")]
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targeyMask;
        [SerializeField] private IUndead _undead;
        private float _attackTimer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _undead = GetComponentInParent<IUndead>();
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
                if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out RaycastHit hit, _attackRange, _targeyMask))
                {
                    Unit unit = hit.transform.GetComponent<Unit>();
                    unit.TakeDamage(_damage);
                    _attackTimer = 0;
                    if (_undead != null)
                    {
                        //TODO: Slow effect
                    }
                }
            }
        }

        #endregion
    }
}