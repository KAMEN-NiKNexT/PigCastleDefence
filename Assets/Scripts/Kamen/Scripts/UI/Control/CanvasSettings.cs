using UnityEngine;
using UnityEngine.UI;

namespace Kamen.UI
{
    public class CanvasSettings : SingletonComponent<CanvasSettings>
    {
        #region Variables

        private RectTransform _canvasRectTransform;
        private CanvasScaler _canvasScaler;
        private Vector2 _currentReferenceResolution;
        private Vector2 _currentScreenSize;

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
            _canvasRectTransform = gameObject.GetComponent<RectTransform>();
            _canvasScaler = gameObject.GetComponent<CanvasScaler>();
            _currentReferenceResolution = _canvasScaler.referenceResolution;
            _currentScreenSize = _canvasRectTransform.sizeDelta;
        }

        #endregion

        #region Get Methods

        public Vector2 GetCurrentReferenceResolution() => _currentReferenceResolution;
        public Vector2 GetCurrentScreenSize() => _currentScreenSize;

        #endregion
    }
}