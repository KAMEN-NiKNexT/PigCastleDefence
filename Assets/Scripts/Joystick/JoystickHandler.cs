using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Kamen.Joystick
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Variables

        [Header("Components")]
        [SerializeField] private Image _joystickArea;
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystickHandle;

        [Header("Variables")]
        private Vector2 _joystickBackgroundStartPosition;
        protected Vector2 _inputVector;
        private bool _isJoystickActive;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
        }

        #endregion

        #region Control Methods
        private void Switch()
        {
            _isJoystickActive = !_isJoystickActive;

            if (!_isJoystickActive) _joystickBackground.gameObject.SetActive(false);
            else _joystickBackground.gameObject.SetActive(true);
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, Camera.main, out Vector2 joystickPosition))
            {
                _inputVector = new Vector2(joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x, joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);
                _inputVector = _inputVector.magnitude > 1f ? _inputVector.normalized : _inputVector;
                _joystickHandle.rectTransform.anchoredPosition = new Vector2(_inputVector.x * _joystickBackground.rectTransform.sizeDelta.x / 2, _inputVector.y * _joystickBackground.rectTransform.sizeDelta.y / 2);
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            Switch();
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, Camera.main, out Vector2 joystickBackgroundPosition))
            {
                _joystickBackground.rectTransform.anchoredPosition = joystickBackgroundPosition;
            }
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;
            Switch();

            _inputVector = Vector2.zero;
            _joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
        }

        #endregion
    }
}