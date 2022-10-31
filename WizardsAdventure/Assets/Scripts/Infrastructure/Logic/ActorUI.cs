using System;
using Enemy;
using UI;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private UIInventory _inventory;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy() =>
            _health.HealthChanged -= UpdateHpBar;

        public void SwithOnInventorySlots()
        {
            if (_inventory.gameObject.activeSelf == false)
                _inventory.gameObject.SetActive(true);
            else
                _inventory.gameObject.SetActive(false);
        }
        
        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}