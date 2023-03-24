using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Analytics;

namespace PigCastleDefence
{
    public class Geyser : MonoBehaviour
    {
        #region Variables

        [Header("Geyser values")]
        private float _geyserDamage;
        private float _geyserRadius;
        private float _geyserDelay;
        private float _geyserForce;

        #endregion

        #region Conrol Methods

        public void SetGayserValues(float geyserDamage, float geyserRadius, float geyserDelay, float geyserForce)
        {
            _geyserDamage = geyserDamage;
            _geyserRadius = geyserRadius;
            _geyserDelay = geyserDelay;
            _geyserForce = geyserForce;
        }
        public void Activate()
        {
            StartCoroutine(ActivateCoroutine());
        }
        private IEnumerator ActivateCoroutine()
        {
            yield return new WaitForSeconds(_geyserDelay);
            
            // TODO fix this create effect
            //Rigidbody targetRigidbody = _target.GetComponent<Rigidbody>();
            //targetRigidbody.AddForce(Vector3.up * _geyserForce, ForceMode.Impulse);
            //
            //if (_target.TryGetComponent(out IDamageable damageable)) damageable.TakeDamage(_geyserDamage);
            //
            //Destroy(geyser, _geyserDuration);
        }

        #endregion
    }
}