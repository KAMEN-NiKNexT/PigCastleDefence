using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;

namespace PigCastleDefence
{
    public class HealthBarManager : SingletonComponent<HealthBarManager>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private UnityEngine.Camera _camera;

        [Header("Variables")]
        private List<HealthBar> _healthBars = new List<HealthBar>();

        #endregion

        #region Unity Methods

        private void LateUpdate()
        {
            LookAtCamera();
        }

        #endregion

        #region Control Methods

        private void LookAtCamera()
        {          
            for (int i = 0; i < _healthBars.Count; i++)
            {
                if (_healthBars[i] != null) _healthBars[i].transform.LookAt(_healthBars[i].transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
            }
        }
        public void AddBar(HealthBar bar) => _healthBars.Add(bar);
        public void DeleteBar(HealthBar bar) => _healthBars.Remove(bar);

        #endregion
    }
}

