using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class Pig : MonoBehaviour
    {
        #region Variables

        [Header("Move Variables")]
        [SerializeField] protected float _speed;
        [SerializeField] protected float _attackRange;

        [Header("Attack Variables")]
        [SerializeField] protected float _damage;
        [SerializeField] private float _pushForce;
        [SerializeField] protected PigAnimationSettings _animationSettings;

        protected Transform _target;

        #endregion

        #region Control Methods

        public void Appear()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _animationSettings.AppearDuration).SetEase(_animationSettings.AppearEase);
            MoveToTarget();
        }

        private Transform FindClosestTarget()
        {
            KdTree<Transform> enemy = new KdTree<Transform>();
            Transform target = enemy.FindClosest(transform.position);
            return target;
        }
        private void MoveToTarget()
        {
            _target = FindClosestTarget();
            StartCoroutine(MoveCoroutine());
        }
        protected virtual IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(_animationSettings.AppearDuration);
            Vector3 direction;
            while (_target != null && Vector3.Distance(transform.position, _target.position) > _attackRange)
            {
                direction = _target.position - transform.position;
                transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime), Quaternion.LookRotation(direction));

                yield return null;
            }

            if (_target != null) Attack();
            else
            {
                Destroy();
            }     
        }
        protected virtual void Attack()
        {
            Vector3 pushDirection = (_target.transform.position - transform.position).normalized;
            _target.GetComponent<Rigidbody>().AddForce(pushDirection * _pushForce, ForceMode.Impulse);
            TakeDamage();
            //Destroy();
        }
        private void TakeDamage()
        {
            if (_target.TryGetComponent(out IDamageable damageable)) damageable.TakeDamage(_damage);
        }
        private void Destroy()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}