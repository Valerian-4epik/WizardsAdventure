using Enemy;
using TMPro;
using UI;
using UnityEngine;

namespace Infrastructure.Logic
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        [SerializeField] private ArmorBar _armorBar;
        [SerializeField] private GameObject _healthPopupText;
        [SerializeField] private GameObject _armorPopupText;
        [SerializeField] private UIInventory _inventory;
        [SerializeField] private InventoryFighter _inventoryFighter;
        [SerializeField] private TMP_Text _armorText;
        [SerializeField] private TMP_Text _weaponText;

        private IHealth _health;

        public GameObject HealthPopupText => _healthPopupText;
        public GameObject ArmorPopupText => _armorPopupText;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
            _health.ArmorChanged += UpdateArmorBar;
            _health.LevelArmorInstalled += SetupLevelArmor;
        }

        private void Awake()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }

            _inventoryFighter.ArmorDressed += ShowArmorLevel;
            _inventoryFighter.WeaponDressed += ShowWeaponLevel;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHpBar;
            _health.ArmorChanged -= UpdateArmorBar;
        }

        public void SetupLevelArmor(int level) => _armorBar.SetLevelValue(level);

        public void SwithOnInventorySlots()
        {
            if (_inventory.gameObject.activeSelf == false)
                _inventory.gameObject.SetActive(true);
            else
                _inventory.gameObject.SetActive(false);
        }

        private void ShowArmorLevel(ItemInfo itemInfo) => _armorText.text = itemInfo.Level.ToString();
        private void ShowWeaponLevel(ItemInfo itemInfo) => _weaponText.text = itemInfo.Level.ToString();

        private void UpdateArmorBar() => 
            _armorBar.SetValue(_health.CurrentArmor, _health.MaxArmor);

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}