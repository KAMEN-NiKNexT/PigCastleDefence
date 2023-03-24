using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class AirPig : Pig
    {
        #region Variables

        [Header("Air Pig Variables")]
        [SerializeField] private float _spinDuration;
        [SerializeField] private float _spinDegrees;
        [SerializeField] private GameObject _impactEffect;

        #endregion

        #region Attack Methods

        protected override void Attack()
        {
            base.Attack();
            _target.DORotate(transform.eulerAngles + new Vector3(0f, _spinDegrees, 0f), _spinDuration, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad);
            // Rotate the target 540 degrees over the duration of _spinDuration using DOTween
            //_target.DORotate(new Vector3(0f, 540f, 0f), _spinDuration).SetEase(_spinEase);

        }

        #endregion
    }
}