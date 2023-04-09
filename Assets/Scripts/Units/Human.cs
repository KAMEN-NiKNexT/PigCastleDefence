using System.Collections;
using UnityEngine;

namespace PigCastleDefence
{
    public class Human : Unit, IIgnitable, IStunnable, IManaUser
    {
        #region Variables

        [Header("Ignite Settings")]
        [SerializeField] private float _igniteMultiplier;
        [SerializeField] private float _burningMultiplier;
        [SerializeField] private float _burningInterval;
        private readonly float _delayToBurn = 0.2f;

        [Header("Mana Settings")]
        [SerializeField] private float _maxMana;
        [SerializeField] private float _manaRegenerationAmount;
        private float _currentMana;

        #endregion

        #region Unity Methods

        private void Start()
        {
            //TODO: Change this line
            Birth();
        }

        private void Update()
        {
            RestoreMana(_manaRegenerationAmount);
        }

        #endregion

        #region Ignite Methods

        public void Ignite(float igniteDuration, float igniteDamage)
        {
            TakeDamage(igniteDamage * _igniteMultiplier);
            StartCoroutine(Burning(igniteDuration, igniteDamage));
        }
        public IEnumerator Burning(float igniteDuration, float igniteDamage)
        {
            // TODO: Do burning effect
            yield return new WaitForSeconds(_delayToBurn * _burningInterval);
            for (int i = 0; i < igniteDuration; i++)
            {
                TakeDamage(igniteDamage * _burningMultiplier);
                yield return new WaitForSeconds(_burningInterval);
            }
        }

        #endregion

        #region Stun Methods

        public void Stun(float stunDuration)
        {
            // TODO: Get stun animation
            IsCanBeControlled = false;
            StartCoroutine(WaitToEndStun(stunDuration));
        }
        public IEnumerator WaitToEndStun(float stunDuration)
        {
            // TODO: Stunning animation
            yield return new WaitForSeconds(stunDuration);
            IsCanBeControlled = true;
        }

        #endregion

        #region Mana Methods

        public bool IsCanCastSpell(float manaForSpell) => _currentMana >= manaForSpell;
        public void UseMana(float amount) => _currentMana = Mathf.Max(0, _currentMana + amount);
        public void RestoreMana(float amount) => _currentMana = Mathf.Clamp(_currentMana + amount, 0, _maxMana);

        #endregion
    }
}