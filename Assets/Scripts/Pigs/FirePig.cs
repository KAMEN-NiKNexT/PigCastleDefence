using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace PigCastleDefence
{
    public class FirePig : Pig
    {
        [Header("Ignite Variables")]
        [SerializeField] private float _igniteDuration;
        [SerializeField] private float _igniteDamage;
        [SerializeField] private float _igniteRadius;
        [SerializeField] private GameObject _explosionPrefab;

        protected override void Attack()
        {
            base.Attack();

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, _igniteRadius);
            foreach (Collider col in colliders)
            {
                if (col.TryGetComponent(out IIgnitable ignitable)) ignitable.Ignite(_igniteDuration, _igniteDamage);
            }
        }
    }
}