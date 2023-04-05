using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

namespace PigCastleDefence
{
    public class Arrow : MonoBehaviour
    {
        //TODO: FIX whole this script
        #region Variables

        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _gravity;
        private Vector3 _direction;
        private Vector3 _velocity;
        private bool _isShot;

        #endregion

        #region Control Methods

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetGravity(float gravity)
        {
            _gravity = gravity;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public void Shoot()
        {
            _isShot = true;
            _velocity = _direction * _speed;
        }

        #endregion

        #region Unity Methods

        private void FixedUpdate()
        {
            if (_isShot)
            {
                _velocity.y -= _gravity * Time.deltaTime;
                transform.position += _velocity * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(_velocity);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //Destroy(gameObject);
        }
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Unit unit = hit.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }

        #endregion
    }
}