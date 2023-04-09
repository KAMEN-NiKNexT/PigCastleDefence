using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PigCastleDefence
{
    public class HealthBar : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Unit _owner;
        [Space]
        [SerializeField] private Image _barBackground;
        [SerializeField] private Image _barFill;
        [SerializeField] private TextMeshProUGUI _healthValueText;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            _owner.OnHealthChanged -= UpdateBar;
            HealthBarManager.Instance.DeleteBar(this);
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            HealthBarManager.Instance.AddBar(this);
            _owner.OnHealthChanged += UpdateBar;
        }
        private void UpdateBar(float maxHealth, float currentHealth)
        {
            _healthValueText.text = Mathf.Round(currentHealth).ToString();
            _barFill.fillAmount = currentHealth / maxHealth;
        }

        #endregion
    }
}

