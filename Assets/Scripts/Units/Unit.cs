using UnityEngine;

namespace PigCastleDefence
{
    public class Unit : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("Health Settings")]
        [SerializeField] protected float _maxHealth;
        protected float _currentHealth;

        [Header("Armor Settings")]
        [SerializeField] protected float _standardArmor;
        protected float _currentArmor;

        [Header("Important Settings")]
        protected bool _isCanBeControlled;

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
        }
        public virtual void TakeDamage(float damage)
        {
            float damageAfterArmor = Armor.CalculateDamageAfterArmor(damage, _currentArmor);
            _currentHealth = Mathf.Max(0, _currentHealth - damageAfterArmor);
            if (_currentHealth <= 0) Die();
        }
        protected void Die()
        {
            // TODO: Implement death logic here.
            Destroy(gameObject);
        }

        #endregion

        #region Set Methods

        public void SetHealth() => _currentHealth = _maxHealth;
        public void SetArmor() => _currentArmor = _standardArmor;

        #endregion
    }
}