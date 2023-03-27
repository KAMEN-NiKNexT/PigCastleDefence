using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PigCastleDefence
{
    public class Pig : MonoBehaviour
    {
        #region Variables

        [Header("Move Variables")]
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected float _rotationSpeed;
        [SerializeField] protected float _attackRange;
        [SerializeField] protected NavMeshAgent _agent;

        [Header("Attack Variables")]
        [SerializeField] protected float _damage;
        [SerializeField] private float _pushForce;
        [SerializeField] protected PigAnimationSettings _animationSettings;

        [Header("Spawn Settings")]
        [SerializeField] protected float _manaCost;

        protected Unit _target;

        #endregion

        #region Properties

        public float ManaCost { get => _manaCost; }

        #endregion

        #region Control Methods

        public void Appear()
        {
            // TODO: Good appear method
            //transform.localScale = Vector3.zero;
            //transform.DOScale(Vector3.one, _animationSettings.AppearDuration).SetEase(_animationSettings.AppearEase);
            StartCoroutine(MoveToTarget());
        }

        protected virtual IEnumerator MoveToTarget()
        {
            _target = EnemiesManager.Instance.GetClosestEnemy(transform.position);
            yield return new WaitForSeconds(_animationSettings.AppearDuration);

            while (_target != null && Vector3.Distance(transform.position, _target.transform.position) > _attackRange)
            {
                _agent.SetDestination(_target.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, _agent.transform.rotation, _rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            if (_target != null) Attack();
            else Destroy();
        }
        protected virtual void Attack()
        {
            Vector3 pushDirection = (_target.transform.position - transform.position).normalized;
            pushDirection.y = 0;
            _target.GetComponent<Rigidbody>().AddForce(pushDirection * _pushForce, ForceMode.Impulse);
            if (_target.TryGetComponent(out IDamageable damageable)) damageable.TakeDamage(_damage);
            Destroy();
        }
        private void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}