using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class Fists : Weapon
    {
        #region Variables

        [Header("Fists Settigns")]
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targeyMask;
        [SerializeField] private IUndead _undead;

        #endregion

        #region Control Methods

        public override void Attack()
        {
            if (_attackTimer >= _attackSpeed)
            {
                base.Attack();
                if (Physics.Raycast(transform.position, transform.rotation * Vector3.forward, out RaycastHit hit, _attackRange, _targeyMask))
                {
                    _attackTimer = 0;
                    StartCoroutine(WaitToHit(hit));
                }
            }
        }
        private IEnumerator WaitToHit(RaycastHit hit)
        {
            yield return new WaitForSeconds(0.5f);
            Unit unit = hit.transform.GetComponent<Unit>();
            unit.TakeDamage(_damage);
            StartCoroutine(UpdateTimer());
            if (_undead != null)
            {
                //TODO: Slow effect
            }
        }

        #endregion
    }
}