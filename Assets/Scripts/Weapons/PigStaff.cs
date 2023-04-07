using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class PigStaff : Weapon
    {
        #region Variables

        [Header("Pig Staff Settings")]
        [SerializeField] private Pig _magicPigPrefab;
        [SerializeField] private float _pigSpawnDistance = 1f;

        [Header("Player Variables")]
        [SerializeField] private GameObject _manaUserObject;
        private IManaUser _manaUser;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            base.Start();
            _manaUser = _manaUserObject.GetComponent<IManaUser>();
        }

        #endregion

        #region Control Methods

        public override void Attack()
        {
            // TODO: Do right pig cost
            if (_manaUser != null && _attackTimer >= _attackSpeed && _manaUser.IsCanCastSpell(10) && EnemiesManager.Instance.IsEnemiesOnTheMap())
            {
                OnAttacked?.Invoke();
                _manaUser.UseMana(10);
                Pig pig = Instantiate(_magicPigPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), Quaternion.identity);
                pig.Appear();
                _attackTimer = 0;
                StartCoroutine(UpdateTimer());
            }
        }

        #endregion

    }
}