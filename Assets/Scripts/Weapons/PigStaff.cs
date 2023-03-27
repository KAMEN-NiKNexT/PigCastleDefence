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
        [SerializeField] private float _spawnDelay;
        private float _spawnTimer = -1f;

        [Header("Player Variables")]
        [SerializeField] private GameObject _manaUserObject;
        private IManaUser _manaUser;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _spawnTimer = _spawnDelay;
            _manaUser = _manaUserObject.GetComponent<IManaUser>();
        }
        private void Update()
        {
            if (_spawnTimer < _spawnDelay) _spawnTimer += Time.deltaTime;
        }

        #endregion

        #region Control Methods

        public override void Attack()
        {
            // TODO: Do right pig cost
            if (_manaUser != null && _spawnTimer >= _spawnDelay && _manaUser.IsCanCastSpell(10) && EnemiesManager.Instance.IsEnemiesOnTheMap())
            {
                _manaUser.UseMana(10);
                Pig pig = Instantiate(_magicPigPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), Quaternion.identity);
                pig.Appear();
                _spawnTimer = 0;
            }
        }

        #endregion

    }
}