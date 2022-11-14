using System;
using UnityEngine;
using Wizards;

namespace Enemy
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private WizardAnimator _animator;
        [SerializeField] private float _maxHealth;

        private float _currentHealth;

        public event Action HealthChanged;

        private void Start()
        {
            _currentHealth = _maxHealth;
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

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            // if (_animator != null)
            //     _animator.PlayHit();
            
            HealthChanged?.Invoke();
        }
    }
}