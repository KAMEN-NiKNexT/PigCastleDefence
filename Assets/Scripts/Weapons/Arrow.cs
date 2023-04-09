using PigCastleDefence.Player;
using System.Collections;
using UnityEngine;

namespace PigCastleDefence
{
    public class Arrow : MonoBehaviour
    {
        #region Variables

        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Settings")]
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _baseDamage;

        [Header("Additional Variables")]
        private Vector3 _velocity;

        #endregion

        #region Control Methods

        public void Shoot(Vector3 direction, float speed, float bowDamage)
        {
            _baseDamage += bowDamage;
            _velocity = direction * speed;
            transform.rotation = Quaternion.LookRotation(direction);
            _rigidbody.velocity = _velocity;
            StartCoroutine(TimerToDestroy());
        }
        private IEnumerator TimerToDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);
            Destroy(gameObject);
        }

        #endregion

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && other.TryGetComponent(out IGoodObject goodObject))
            {
                damageable.TakeDamage(_baseDamage);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}