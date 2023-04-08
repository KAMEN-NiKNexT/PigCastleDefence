using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class Phantom : Unit, IIncorporeal, IManaUser
    {
        #region Variables

        [Header("Mana Settings")]
        [SerializeField] private float _maxMana;
        [SerializeField] private float _manaRegenerationAmount;
        private float _currentMana;

        [SerializeField] private float _dodgeChance;
        [SerializeField] private float _dodgeTimer;
        [SerializeField] private float dodgeRadius;
        [SerializeField] private GameObject dodgeEffectPrefab;
        public Action OnDodged;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Birth();
            StartCoroutine(TeleportTimer());
        }

        private void Update()
        {
            RestoreMana(_manaRegenerationAmount);
        }

        #endregion

        #region Phantom Methods

        private IEnumerator TeleportTimer()
        {
            // TODO: Implement teleport logic here.
            yield return new WaitForSeconds(_dodgeTimer);
            Dodge();
            StartCoroutine(TeleportTimer());
        }
        public override void TakeDamage(float damage)
        {
            if (UnityEngine.Random.Range(1, 101) <= _dodgeChance) Dodge();
            else base.TakeDamage(damage);
        }
        public void Dodge()
        {
            Vector3 dodgePosition = UnityEngine.Random.insideUnitSphere * dodgeRadius + transform.position;
            dodgePosition.y = transform.position.y;
            OnDodged?.Invoke();
            //TODO: Create good effect
            //Instantiate(dodgeEffectPrefab, transform.position, Quaternion.identity);
            transform.position = dodgePosition;
            //Instantiate(dodgeEffectPrefab, transform.position, Quaternion.identity);
        }

        #endregion

        #region Mana Methods

        public bool IsCanCastSpell(float manaForSpell) => _currentMana >= manaForSpell;
        public void UseMana(float amount) => _currentMana = Mathf.Max(0, _currentMana - amount);
        public void RestoreMana(float amount) => _currentMana = Mathf.Clamp(_currentMana + amount, 0, _maxMana);

        #endregion
    }
}
