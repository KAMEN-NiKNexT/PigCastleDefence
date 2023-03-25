using UnityEngine;

namespace PigCastleDefence.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        #region Variables

        [Header("Variables")]
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

        public void SpawnPig()
        {
            // TODO: Do right pig cost
            if (_spawnTimer >= _spawnDelay && _manaUser.IsCanCastSpell(10))
            {
                _manaUser.UseMana(10);
                Instantiate(_magicPigPrefab, transform.position + new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f)), Quaternion.identity);
                _spawnTimer = 0;
            }
        }

        #endregion
    }
}