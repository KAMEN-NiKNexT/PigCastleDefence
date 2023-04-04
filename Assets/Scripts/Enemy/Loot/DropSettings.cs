using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    [CreateAssetMenu(fileName = "DropSettings", menuName = "PigCastleDefence/Animation/DropSettings", order = 1)]
    public class DropSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _dropDistance;
        [SerializeField] private float _dropForce;
        [SerializeField] private int _dropJumpAmount;
        [SerializeField] private float _dropDuration;
        [SerializeField] private float _delayBeforeMoveToPlayer;
        [SerializeField] private float _moveToPlayerDuration;
        [SerializeField] private Ease _moveToPlayerEase;

        #endregion

        #region Properties

        public float DropDistance { get => _dropDistance; }
        public float DropForce { get => _dropForce; }
        public int DropJumpAmount { get => _dropJumpAmount; }
        public float DropDuration { get => _dropDuration; }
        public float DelayBeforeMoveToPlayer { get => _delayBeforeMoveToPlayer; }
        public float MoveToPlayerDuration { get => _moveToPlayerDuration; }
        public Ease MoveToPlayerEase { get => _moveToPlayerEase; }

        #endregion
    }
}
