using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace PigCastleDefence
{
    public class Arrow : MonoBehaviour
    {
        //TODO: FIX whole this script
        #region Variables

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        private Vector3 _direction;
        private Vector3 _velocity;
        private bool _isShot;

        #endregion

        #region Control Methods

        public void Shoot(Vector3 direction, float speed, float damage)
        {
            _isShot = true;
            _direction = direction;
            _speed = speed;
            _damage = damage;
            _velocity = _direction * _speed;
            transform.rotation = Quaternion.LookRotation(direction);
            _rigidbody.velocity = _velocity;
        }

        #endregion

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Unit unit) && other.TryGetComponent(out PlayerController playerController))
            {
                unit.TakeDamage(_damage);
            }
        }

        #endregion
    }
}