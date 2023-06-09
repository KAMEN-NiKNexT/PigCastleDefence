using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

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
            _agent.speed = _moveSpeed;
            _agent.angularSpeed = _rotationSpeed;
            StartCoroutine(WaitToEndAppear());
        }

        protected virtual IEnumerator WaitToEndAppear()
        {
            yield return new WaitForSeconds(_animationSettings.AppearDuration);
            StartCoroutine(MoveToTarget());
        }
        protected virtual IEnumerator MoveToTarget()
        {
            _target = EnemiesManager.Instance.GetClosestEnemy(transform.position);
            while (_target != null && Vector3.Distance(transform.position, _target.transform.position) > _attackRange)
            {
                Vector3 check = _target.transform.position;
                check.y = 0.6f;
                _agent.SetDestination(check);

                if (_agent.velocity.magnitude > 0.1f)
                {
                    float targetAngle = Mathf.Atan2(_agent.velocity.x, _agent.velocity.z) * Mathf.Rad2Deg;
                    Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                    transform.rotation = targetRotation;
                }

                yield return null;
            }

            if (_target != null) Attack();
            else if (_target == null && EnemiesManager.Instance.IsEnemiesOnTheMap()) StartCoroutine(MoveToTarget());
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