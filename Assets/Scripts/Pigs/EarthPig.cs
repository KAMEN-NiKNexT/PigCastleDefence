using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class EarthPig : Pig
    {
        [Header("Nora Variables")]
        [SerializeField] private float _burrowDuration;
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private float _stunDuration;
        [SerializeField] private PigAnimationSettings _burrowAnimation;

        private bool _isBurrowing;

        protected override void Attack()
        {
            base.Attack();
            StunTarget();
        }

        protected override IEnumerator MoveToTarget()
        {
            _isBurrowing = true;

            // TODO: Borrow in earth
            //yield return new WaitForSeconds(_burrowAnimation.DisappearDuration);

            // Find a new target while burrowed
            //_target = FindClosestTarget();

            // Move underground towards target
            Vector3 targetPosition = _target.transform.position;
            targetPosition.y = transform.position.y; // Keep same Y position as the pig
            transform.DOMove(targetPosition, _burrowDuration).SetEase(Ease.Linear);

            // Wait for pig to finish burrowing
            yield return new WaitForSeconds(_burrowDuration);

            // TODO: Appear from earth

            Attack();
        }

        private void StunTarget()
        {
            if (_target.TryGetComponent(out IStunnable stunnable)) stunnable.Stun(_stunDuration);
        }
    }
}