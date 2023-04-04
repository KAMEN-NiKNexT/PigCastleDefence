using System;
using System.Collections;
using UnityEngine;

namespace PigCastleDefence
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("Health Settings")]
        [SerializeField] protected float _maxHealth;
        protected float _currentHealth;

        [Header("Armor Settings")]
        [SerializeField] protected float _standardArmor;
        protected float _currentArmor;

        [Header("Important Settings")]
        public Action<float, float> OnHealthChanged;
        public Action<Unit> OnUnitDied;
        public bool IsCanBeControlled { get; protected set; }

        #endregion

        #region Properties

        public float MaxHealth { get => _maxHealth; }
        public float CurrentHealth { get => _currentHealth; }
        public float StandardArmor { get => _standardArmor; }
        public float CurrentArmor { get => _currentArmor; }

        #endregion

        #region Control Methods

        public void Birth()
        {
            // TODO: Implement birth logic here.
            IsCanBeControlled = true;
            SetHealth();
            SetArmor();
            OnHealthChanged?.Invoke(_maxHealth, _currentHealth);
        }
        public virtual void TakeDamage(float damage)
        {
            float damageAfterArmor = Armor.CalculateDamageAfterArmor(damage, _currentArmor);
            _currentHealth = Mathf.Max(0, _currentHealth - damageAfterArmor);
            OnHealthChanged?.Invoke(_maxHealth, _currentHealth);
            if (_currentHealth <= 0) Die();
        }
        public virtual void Heal(int newHealth)
        {
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + newHealth);
            OnHealthChanged?.Invoke(_maxHealth, _currentHealth);
        }
        protected void Die()
        {
            // TODO: Implement death logic here.
            IsCanBeControlled = false;
            OnUnitDied?.Invoke(this);
            StartCoroutine(WaitToDestroy());
        }
        private IEnumerator WaitToDestroy()
        {
            //TODO: Fix this method
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }

        #endregion

        #region Set Methods

        public void SetHealth() => _currentHealth = _maxHealth;
        public void SetArmor() => _currentArmor = _standardArmor;

        #endregion
    }
}