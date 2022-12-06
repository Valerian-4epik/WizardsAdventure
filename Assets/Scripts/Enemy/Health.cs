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
        [SerializeField] private Transform _popupTextPoint;
        [SerializeField] private AudioPlayerForWizard _audioPlayer;
        [SerializeField] private ActorUI _actorUI;

        private float _currentHealth;
        private float _currentArmor;
        private bool _isDead = false;
        private Effector _effector;

        public event Action HealthChanged;
        public event Action ArmorChanged;
        public event Action<int> LevelArmorInstalled;

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

        public bool IsDead => _isDead;

        private void Start()
        {
            _effector = GetComponent<Effector>();
            _maxHealth = _currentHealth;
            _currentArmor = _maxArmor;
        }

        public void SetHealth(int value)
        {
            _currentHealth += value;
            _maxHealth = _currentHealth;
        }

        public void AssignArmor(float value, int level)
        {
            MaxArmor = value;
            CurrentArmor = MaxArmor;
            ArmorChanged?.Invoke();
            LevelArmorInstalled?.Invoke(level);
            _actorUI.SetupLevelArmor(level);
        }

        public void RefreshValue() => CurrentArmor = 0;

        public void TakeDamage(float damage)
        {
            if (_currentArmor > 0)
            {
                _currentArmor -= damage;
                _effector.PlayArmorDefenceEffect();
                _audioPlayer.PLayHitArmorSound();
                PlayPopupText(damage, GetArmorPopupText());
                ArmorChanged?.Invoke();
            }
            else
            {
                _currentHealth -= damage;
                _effector.PlayHitEffect();
                _audioPlayer.PLayHitSound();
                PlayPopupText(damage, GetHealthPopupText());
                HealthChanged?.Invoke();
                CheckIsDead();
            }
        }

        public void CheckReadiness()
        {
            HealthChanged?.Invoke();
            ArmorChanged?.Invoke();
            // LevelArmorInstalled?.Invoke(_);
        }

        private void CheckIsDead()
        {
            if (_currentHealth <= 0)
                _isDead = true;
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