using PigCastleDefence.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;

namespace PigCastleDefence
{
    public class EnemiesManager : SingletonComponent<EnemiesManager>
    {
        #region Variables

        [Header("Spawn settings")]
        [SerializeField] private Unit _zombiePrefab;
        [SerializeField] private Unit _barbarianPrefab;
        [SerializeField] private int _zombieAmout;
        [SerializeField] private int _baribarianAmout;
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private Transform _spawnPoint;

        private readonly KdTree<Unit> _enemies = new KdTree<Unit>();
        public GameObject _player;
        //private List<Unit>

        #endregion

        #region Control Methods

        private void Start()
        {
            StartCoroutine(SpawnEnemies(_zombiePrefab, _zombieAmout));
            StartCoroutine(SpawnEnemies(_barbarianPrefab, _baribarianAmout));
        }
        private IEnumerator SpawnEnemies(Unit unit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Unit enemy = Instantiate(unit, spawnPosition, Quaternion.identity);
                enemy.Birth();
                enemy.OnUnitDied += RemoveEnemy;
                enemy.GetComponent<EnemyMovement>().SetTarget(_player.transform);
                _enemies.Add(enemy);

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + _spawnPoint.position;
            return spawnPosition;
        }
        private void RemoveEnemy(Unit enemy) => _enemies.Remove(enemy);
        public Unit GetClosestEnemy(Vector3 position) => _enemies.FindClosest(position);
        public bool IsEnemiesOnTheMap() => _enemies.Count > 0;

        #endregion
    }
}