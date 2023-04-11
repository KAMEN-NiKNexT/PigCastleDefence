using PigCastleDefence.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using System;
using Random = UnityEngine.Random;

namespace PigCastleDefence
{
    public class EnemiesManager : SingletonComponent<EnemiesManager>
    {
        #region Classes

        [Serializable] private struct EnemySpawner
        {
            #region EnemySpawner Variables

            [Header("Spawn settings")]
            [SerializeField] private string _unitName;
            [SerializeField] private Unit _spawnUnit;
            [SerializeField] private Transform _spawnPoint;
            [SerializeField] private float _spawnRadius;
            private EnemyData _enemyData;
            private bool _isEnable;

            #endregion

            #region EnemySpawner Properties

            public string UnitName { get => _unitName; }
            public Unit SpawnUnit { get => _spawnUnit; }
            public Transform SpawnPoint { get => _spawnPoint; }
            public float SpawnRadius { get => _spawnRadius; }
            public EnemyData EnemyData 
            { 
                get => _enemyData; 
                set
                {
                    if (value == null) _isEnable = false;
                    else
                    {
                        _enemyData = value;
                        _isEnable = true;
                    }
                }
            }
            public bool IsEnable { get => _isEnable; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private EnemySpawner[] _enemySpawners;
        [SerializeField] private Level[] _levels;

        private readonly KdTree<Unit> _enemies = new KdTree<Unit>();
        public GameObject _player;

        #endregion

        #region Control Methods

        private void Start()
        {
            SetUpEnemySpawner();
            CreateEnemy();
        }
        private void CreateEnemy()
        {
            for (int i = 0; i < _enemySpawners.Length; i++)
            {
                if (_enemySpawners[i].IsEnable) StartCoroutine(SpawnEnemies(_enemySpawners[i]));
            }
        }
        private void SetUpEnemySpawner()
        {
            for (int i = 0; i < _enemySpawners.Length; i++)
            {
                _enemySpawners[i].EnemyData = _levels[0].GetData(_enemySpawners[i].UnitName);
            }
        }
        private IEnumerator SpawnEnemies(EnemySpawner enemySpawner)
        {
            yield return new WaitForSeconds(enemySpawner.EnemyData.DelayBeforeStartSpawn);
            for (int i = 0; i < enemySpawner.EnemyData.Amount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(enemySpawner.SpawnPoint, enemySpawner.SpawnRadius);
                Unit enemy = Instantiate(enemySpawner.SpawnUnit, spawnPosition, Quaternion.identity);
                enemy.Birth();
                enemy.OnUnitDied += RemoveEnemy;
                enemy.GetComponent<EnemyMovement>().SetTarget(_player.transform);
                _enemies.Add(enemy);

                yield return new WaitForSeconds(enemySpawner.EnemyData.DelayBetweenSpawn);
            }
        }

        private Vector3 GetRandomSpawnPosition(Transform point, float radius)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector3 spawnPosition = point.position + new Vector3(randomCircle.x, 0f, randomCircle.y);
            return spawnPosition;
        }
        private void RemoveEnemy(Unit enemy) => _enemies.Remove(enemy);
        public Unit GetClosestEnemy(Vector3 position) => _enemies.FindClosest(position);
        public bool IsEnemiesOnTheMap() => _enemies.Count > 0;

        #endregion
    }
}