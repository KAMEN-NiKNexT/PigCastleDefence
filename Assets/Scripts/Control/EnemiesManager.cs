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

        //TODO: Rename this struct
        [Serializable] private struct EnemySpawner
        {
            #region EnemySpawner Variables

            [Header("Spawn settings")]
            [SerializeField] private string _unitName;
            [SerializeField] private Unit _spawnUnit;
            [SerializeField] private Transform _spawnPoint;
            [SerializeField] private int _amount;
            [SerializeField] private float _spawnDelay;
            [SerializeField] private float _spawnRadius;
            [SerializeField] private float _spawnTime;

            #endregion

            #region EnemySpawner Properties

            public string UnitName { get => _unitName; }
            public Unit SpawnUnit { get => _spawnUnit; }
            public Transform SpawnPoint { get => _spawnPoint; }
            public int Amount { get => _amount; }
            public float SpawnDelay { get => _spawnDelay; }
            public float SpawnRadius { get => _spawnRadius; }
            public float SpawnTime { get => _spawnTime; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

        private readonly KdTree<Unit> _enemies = new KdTree<Unit>();
        public GameObject _player;
        //private List<Unit>

        #endregion

        #region Control Methods

        private void Start()
        {
            for(int i = 0; i < _enemySpawners.Count; i++)
            {
                StartCoroutine(SpawnEnemies(_enemySpawners[i]));
            }
        }
        private IEnumerator SpawnEnemies(EnemySpawner enemySpawner)
        {
            yield return new WaitForSeconds(enemySpawner.SpawnDelay);
            for (int i = 0; i < enemySpawner.Amount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition(enemySpawner.SpawnPoint, enemySpawner.SpawnRadius);
                Unit enemy = Instantiate(enemySpawner.SpawnUnit, spawnPosition, Quaternion.identity);
                enemy.Birth();
                enemy.OnUnitDied += RemoveEnemy;
                enemy.GetComponent<EnemyMovement>().SetTarget(_player.transform);
                _enemies.Add(enemy);

                yield return new WaitForSeconds(enemySpawner.SpawnTime);
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