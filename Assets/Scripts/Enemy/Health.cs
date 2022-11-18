using System;
using Infrastructure.Logic;
using UI;
using UnityEngine;
using Wizards;

namespace Enemy
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private WizardAnimator _animator;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _maxArmor;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private Transform _popupTextPoint;

        private float _currentHealth;
        private float _currentArmor;

        public event Action HealthChanged;
        public event Action ArmorChanged;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _currentArmor = _maxArmor;
        }

        public float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public float CurrentArmor
        {
            get => _currentArmor;
            set
            {
                _currentArmor = value;
                ArmorChanged?.Invoke();
            }
        }

        public float MaxArmor { get => _maxArmor; set => _maxArmor = value; }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            PlayHitEffect(damage);
            HealthChanged?.Invoke();
        }

        private void PlayHitEffect(float value)
        {
            var effectObject = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            effectObject.GetComponent<ParticleSystem>().Play();
            PlayPopupText(value);
        }

        private void PlayPopupText(float value)
        {
            var popupText = Instantiate(GetPopupText(), _popupTextPoint.position, Quaternion.identity);
            popupText.GetComponent<FloatingText>().SetValue(value);
        }

        private GameObject GetPopupText()
        {
            var actorUI = gameObject.GetComponent<ActorUI>();
            return actorUI.PopupText;
        }
    }
}