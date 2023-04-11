using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    [Serializable]
    public class EnemyData
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private int _amount;
        [SerializeField] private float _delayBeforeStartSpawn;
        [SerializeField] private float _delayBetweenSpawn;

        #endregion

        #region Properties

        public string Name { get => _name; }
        public int Amount { get => _amount; }
        public float DelayBeforeStartSpawn { get => _delayBeforeStartSpawn; }
        public float DelayBetweenSpawn { get => _delayBetweenSpawn; }

        #endregion
    }
}