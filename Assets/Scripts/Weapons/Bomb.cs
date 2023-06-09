using System.Collections;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class Bomb : Weapon
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _timeBeforeExplosion;
        [SerializeField] private float _explosionDelay;
        [SerializeField] private float _minExplosionRadius;
        [SerializeField] private float _maxExplosionRadius;
        [SerializeField] private float _minExplosionDamage;
        [SerializeField] private float _maxExplosionDamage;
        [SerializeField] private float _igniteDamage;
        [SerializeField] private float _igniteDuration;
        [Space]
        [SerializeField] private Transform _bombIncreaseModel; //TODO: rename this variable
        [SerializeField] private GameObject _explosionEffect;
        [SerializeField] private Vector3 _minExplosionSize;
        [SerializeField] private Vector3 _maxExplosionSize;
        [SerializeField] private Vector3 _minSize;
        [SerializeField] private Vector3 _maxSize;

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
            _owner.OnUnitDied += ExplosionCall;
            _timerCoroutine = StartCoroutine(Timer());
        }
        private IEnumerator Timer()
        {
            while (_currentTime < _timeBeforeExplosion)
            {
                _currentTime += Time.deltaTime;
                //TODO: Change with do tween
                _bombIncreaseModel.transform.localScale = Vector3.Lerp(_minSize, _maxSize, _currentTime / _timeBeforeExplosion);
                yield return null;
            }
            _currentTime = _timeBeforeExplosion;
            ExplosionCall(_owner);
        }
        public override void Attack() => StartCoroutine(Explosion());
        private void ExplosionCall(Unit unit) => StartCoroutine(Explosion());
        private IEnumerator Explosion() 
        {
            yield return new WaitForSeconds(_explosionDelay);

            _owner.OnUnitDied -= ExplosionCall;
            _owner.TakeDamage(int.MaxValue);
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);

            // TODO: Add explosion effect
            float explosionPower = _currentTime / _timeBeforeExplosion;

            GameObject explosionEffect = Instantiate(_explosionEffect);
            explosionEffect.transform.position = transform.position;
            explosionEffect.transform.localScale = Vector3.Lerp(_minExplosionSize, _maxExplosionSize, explosionPower);

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