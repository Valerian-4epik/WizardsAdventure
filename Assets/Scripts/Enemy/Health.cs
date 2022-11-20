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
        public event Action<int> LevelArmorInstalled;

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

        public float MaxArmor
        {
            get => _maxArmor;
            set => _maxArmor = value;
        }

        public void AssignArmor(float value, int level)
        {
            MaxArmor = value;
            CurrentArmor = MaxArmor;
            ArmorChanged?.Invoke();
            LevelArmorInstalled?.Invoke(level);
            Debug.Log(gameObject.name + "Включаем");
        }

        public void RefreshValue() => CurrentArmor = 0;

        public void TakeDamage(float damage)
        {
            if (_currentArmor > 0)
            {
                _currentArmor -= damage;
                PlayHitEffect(damage);
                PlayPopupText(damage, GetArmorPopupText());
                ArmorChanged?.Invoke();
            }
            else
            {
                _currentHealth -= damage;
                PlayHitEffect(damage);
                PlayPopupText(damage, GetHealthPopupText());
                HealthChanged?.Invoke();
            }
        }

        public void CheckReadiness()
        {
            HealthChanged?.Invoke();
            
            if (_currentArmor == 0)
            {
                ArmorChanged?.Invoke();
            }
        }

        private void PlayHitEffect(float value)
        {
            var effectObject = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            effectObject.GetComponent<ParticleSystem>().Play();
        }

        private void PlayPopupText(float value, GameObject poputText)
        {
            var popupText = Instantiate(poputText, _popupTextPoint.position, Quaternion.identity);
            popupText.GetComponent<FloatingText>().SetValue(value);
        }

        private GameObject GetHealthPopupText()
        {
            var actorUI = gameObject.GetComponent<ActorUI>();
            return actorUI.HealthPopupText;
        }

        private GameObject GetArmorPopupText()
        {
            var actorUI = gameObject.GetComponent<ActorUI>();
            return actorUI.ArmorPopupText;
        }
    }
}