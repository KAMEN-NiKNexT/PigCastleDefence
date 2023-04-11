using UnityEngine.AI;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace PigCastleDefence.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        #region Variables

        [Header("Components")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Rigidbody _rigidbody;

        [Header("Movement settings")]
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected float _rotationSpeed;
        [SerializeField] protected float _maxTargetDistance;

        [Header("Additional variables")]
        private Transform _target;
        private Vector3 _targetDirection;
        private Vector3 _moveDirection;
        private float _distanceFromTarget;
        public Action OnMove;

        #endregion

        #region Control Methods

        public virtual bool MoveToTarget()
        {
            _distanceFromTarget = Vector3.Distance(_target.transform.position, transform.position);
            if (_distanceFromTarget > _maxTargetDistance)
            {
                OnMove?.Invoke();
                _agent.enabled = true;
                _agent.SetDestination(_target.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, _agent.transform.rotation, _rotationSpeed / Time.deltaTime);

                _moveDirection = transform.forward * _moveSpeed;
                _moveDirection.y = _rigidbody.velocity.y;
                _rigidbody.velocity = _moveDirection;

                ResetAgent();
                return true;
            }
            else
            {
                _agent.enabled = false;
                _targetDirection = _target.transform.position - transform.position;
                _targetDirection.y = 0;
                _targetDirection.Normalize();
                if (_targetDirection == Vector3.zero) _targetDirection = transform.forward;

                Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed / Time.deltaTime);

                ResetAgent();
                return false;
            }
        }
        private void ResetAgent()
        {
            _agent.transform.localPosition = Vector3.zero;
            _agent.transform.localRotation = Quaternion.identity;
        }
        public void SetTarget(Transform target) => _target = target;

        #endregion
    }
}