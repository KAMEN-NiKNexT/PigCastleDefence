using UnityEngine;
using Kamen;

namespace PigCastleDefence
{
    public class MyGravity : SingletonComponent<MyGravity>
    {
        #region Variables

        [Header("Settings")]
        private Vector3 _gravityForce;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            SetGravityForce();
        }

        #endregion

        #region Control Methods

        public float GetGravityHandling(bool isGrounded)
        {
            if (!isGrounded) return _gravityForce.y;
            else return 0;
        }
        private void SetGravityForce() => _gravityForce = Physics.gravity;

        #endregion
    }
}