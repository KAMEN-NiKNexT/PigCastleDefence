using DG.Tweening;
using PigCastleDefence.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace PigCastleDefence
{
    public class LootDropSystem : MonoBehaviour
    {
        #region Classes 

        [System.Serializable] private struct LootTable
        {
            #region LootTable Variables

            [SerializeField] private Item _item;
            [SerializeField] private int _minCount;
            [SerializeField] private int _maxCount;
            [Range(0, 100)][SerializeField] private int _dropChance;

            #endregion

            #region LootTable Properties

            public Item Item { get => _item; }
            public int MinCount { get => _minCount; }
            public int MaxCount { get => _maxCount; }
            public int DropChance { get => _dropChance; }

            #endregion
        }

        #endregion

        #region Variables

        [SerializeField] private Unit _owner;
        [SerializeField] private LootTable[] _lootTable;
        [SerializeField] private LayerMask _groundMask;
        private const int _searchGroundRange = 20;
        [SerializeField] private DropSettings _dropSettings;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _owner.OnUnitDied += Drop;
        }
        #endregion

        #region Control Methods

        private void Drop(Unit owner)
        {
            _owner.OnUnitDied -= Drop;
            for (int i = 0; i < _lootTable.Length; i++)
            {
                LootTable loot = _lootTable[i];
                if (Random.Range(0, 100) < loot.DropChance)
                {
                    int count = Random.Range(loot.MinCount, loot.MaxCount + 1);
                    for (int j = 0; j < count; j++)
                    {
                        Item item = Instantiate(_lootTable[i].Item);
                        item.transform.position = transform.position;
                        StartCoroutine(DropItemCoroutine(item));
                    }
                }
            }
        }
        private IEnumerator DropItemCoroutine(Item item)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _searchGroundRange, _groundMask))
            {
                Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(-_dropSettings.DropDistance, _dropSettings.DropDistance), 
                                                   hit.point.y, 
                                                   transform.position.z + Random.Range(-_dropSettings.DropDistance, _dropSettings.DropDistance));

                item.transform.DOJump(dropPosition, _dropSettings.DropForce, _dropSettings.DropJumpAmount, _dropSettings.DropDuration);
            }
            yield return new WaitForSeconds(_dropSettings.DropDuration + _dropSettings.DelayBeforeMoveToPlayer);
            item.transform.DOMove(EnemiesManager.Instance._player.transform.position, _dropSettings.MoveToPlayerDuration).SetEase(_dropSettings.MoveToPlayerEase);
            yield return new WaitForSeconds(_dropSettings.MoveToPlayerDuration);
            item.Use();
            Destroy(item.gameObject);
        }

        #endregion
    }
}
