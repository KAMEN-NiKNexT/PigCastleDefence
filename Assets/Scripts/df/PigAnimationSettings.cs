using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    [CreateAssetMenu(fileName = "PigAnimationSettings", menuName = "PigCastleDefence/Animation/PigAnimationSettings", order =1)]
    public class PigAnimationSettings : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _appearDuration;
        [SerializeField] private Ease _appearEase;

        #endregion

        #region Properties

        public float AppearDuration { get => _appearDuration; }
        public Ease AppearEase { get => _appearEase; }

        #endregion
    }
}