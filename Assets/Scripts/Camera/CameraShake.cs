using System.Collections;
using UnityEngine;
using Cinemachine;
using Kamen;

namespace PigCastleDefence.Camera
{
    public class CameraShake : SingletonComponent<CameraShake>
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private CinemachineVirtualCamera _camera;
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _multiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        public void Shake(float amplitude, float frequency, float duration)
        {
            _multiChannelPerlin.m_AmplitudeGain = amplitude;
            _multiChannelPerlin.m_FrequencyGain = frequency;
            StartCoroutine(EndShake(duration));
        }
        private IEnumerator EndShake(float duration)
        {
            yield return new WaitForSeconds(duration);
            _multiChannelPerlin.m_AmplitudeGain = 0;
            _multiChannelPerlin.m_FrequencyGain = 0;
        }

        #endregion
    }
}