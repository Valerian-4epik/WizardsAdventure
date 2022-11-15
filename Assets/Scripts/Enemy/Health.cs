using System;
using UnityEngine;
using Wizards;

namespace Enemy
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private WizardAnimator _animator;
        [SerializeField] private float _maxHealth;
        [SerializeField] private GameObject _hitEffect;

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
            PlayHitEffect();
            // if (_animator != null)
            //     _animator.PlayHit();
            
            HealthChanged?.Invoke();
        }

        private void PlayHitEffect()
        {
            var effectObject = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            effectObject.GetComponent<ParticleSystem>().Play();
        }
    }
}