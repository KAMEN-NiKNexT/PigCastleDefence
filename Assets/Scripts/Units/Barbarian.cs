using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PigCastleDefence
{
    public class Barbarian : Unit, IIgnitable, IStunnable
    {
        #region Variables

        [Header("Ignite Settings")]
        [SerializeField] private float _igniteMultiplier;
        [SerializeField] private float _burningMultiplier;
        [SerializeField] private float _burningInterval;
        private readonly float _delayToBurn = 0.2f;

        #endregion

        #region Ignite Methods

        public void Ignite(float igniteDuration, float igniteDamage)
        {
            TakeDamage(igniteDamage * _igniteMultiplier);
            StartCoroutine(Burning(igniteDuration, igniteDamage));
        }
        public IEnumerator Burning(float igniteDuration, float igniteDamage)
        {
            // TODO: Do burning effect
            yield return new WaitForSeconds(_delayToBurn * _burningInterval);
            for (int i = 0; i < igniteDuration; i++)
            {
                TakeDamage(igniteDamage * _burningMultiplier);
                yield return new WaitForSeconds(_burningInterval);
            }
        }

        #endregion

        #region Stun Methods

        public void Stun(float stunDuration)
        {
            // TODO: Get stun animation
            IsCanBeControlled = false;
            StartCoroutine(WaitToEndStun(stunDuration));
        }
        public IEnumerator WaitToEndStun(float stunDuration)
        {
            // TODO: Stunning animation
            yield return new WaitForSeconds(stunDuration);
            IsCanBeControlled = true;
        }

        #endregion
    }
}