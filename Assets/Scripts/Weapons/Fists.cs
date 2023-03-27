using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence.Weapons
{
    public class Fists : Weapon
    {
        #region Variables

        [Header("Fists Settigns")]
        [SerializeField] private float _attackSpeed;
        [SerializeField] private IUndead _undead;
        private float _attackTimer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _undead = GetComponentInParent<IUndead>();
        }
        private void Update()
        {
            if (_attackTimer < _attackSpeed) _attackTimer += Time.deltaTime;
        }

        #endregion

        #region Control Methods

        public override void Attack()
        {
            // TODO: Do right attack for fists
            if (_attackTimer >= _attackSpeed)
            {
                //_manaUser.UseMana(10);
                //Instantiate(_magicPigPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), Quaternion.identity);
                //_spawnTimer = 0;
                if (_undead != null)
                {

                }
            }
        }

        #endregion
    }
}