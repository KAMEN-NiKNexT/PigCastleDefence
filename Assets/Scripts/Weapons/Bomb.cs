using System.Collections;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class Bomb : Weapon
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _timeBeforeExplosion;
        [SerializeField] private float _minExplosionRadius;
        [SerializeField] private float _maxExplosionRadius;
        [SerializeField] private float _minExplosionDamage;
        [SerializeField] private float _maxExplosionDamage;
        [SerializeField] private float _igniteDamage;
        [SerializeField] private float _igniteDuration;

        [Header("Variables")]
        private float _currentTime;
        private Coroutine _timerCoroutine;
        private Unit _owner;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _owner = GetComponentInParent<Unit>();
            _owner.OnUnitDied += Explosion;
            _timerCoroutine = StartCoroutine(Timer());
        }
        private IEnumerator Timer()
        {
            while (_currentTime < _timeBeforeExplosion)
            {
                _currentTime += Time.deltaTime;
                yield return null;
            }
            _currentTime = _timeBeforeExplosion;
            Explosion(_owner);
        }
        public override void Attack(){ } //DO nothing
        private void Explosion(Unit unit)
        {
            _owner.OnUnitDied -= Explosion;
            _owner.TakeDamage(int.MaxValue);
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);

            // TODO: Add explosion effect
            float explosionPower = _currentTime / _timeBeforeExplosion;

            Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Lerp(_minExplosionRadius, _maxExplosionRadius, explosionPower));
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable entity)) entity.TakeDamage(Mathf.Lerp(_minExplosionDamage, _maxExplosionDamage, explosionPower));
                if (collider.TryGetComponent(out IIgnitable igniteEntity)) igniteEntity.Ignite(_igniteDuration, _igniteDamage);
            }
        }

        #endregion
    }
}