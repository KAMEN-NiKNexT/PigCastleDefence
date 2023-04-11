using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence 
{
    [CreateAssetMenu(fileName = "Level", menuName = "PigCastleDefence/Game/Level", order = 1)]
    public class Level : ScriptableObject
    {
        #region Variables

        [SerializeField] private EnemyData[] _enemyDatas;

        #endregion

        #region Control Methods

        public EnemyData GetData(string name)
        {
            for (int i = 0; i < _enemyDatas.Length; i++)
            {
                if (_enemyDatas[i].Name == name) return _enemyDatas[i];
            }

            return null;
        }

        #endregion
    }
}