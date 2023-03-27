using PigCastleDefence.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class EnemiesManager : MonoBehaviour
    {
        #region Variables

        [Header("Spawn settings")]
        [SerializeField] private Unit _enemyPrefab;
        [SerializeField] private int _enemiesAmout;
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private Transform _spawnPoint;

        private readonly KdTree<Unit> _enemies = new KdTree<Unit>();
        [SerializeField] private GameObject _player;
        //private List<Unit>

        #endregion

        #region Control Methods

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }
        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _enemiesAmout; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                Unit enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
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

        #endregion
    }
}