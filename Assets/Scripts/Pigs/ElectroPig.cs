using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class ElectroPig : Pig
    {
        [Header("Chain Reaction Variables")]
        [SerializeField] private float _chainRadius;
        [SerializeField] private int _maxChainBounces;
        [SerializeField] private LayerMask _enemyLayerMask;

        private int _currentChainBounces;

        protected override void Attack()
        {
            base.Attack();
            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayerMask);

            foreach (Collider enemy in hitEnemies)
            {
                _currentChainBounces++;

                if (_currentChainBounces >= _maxChainBounces)
                {
                    break;
                }
            }
        }
    }
}