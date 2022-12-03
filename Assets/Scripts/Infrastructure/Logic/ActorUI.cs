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
        [SerializeField] private GameObject _armorPanel;
        [SerializeField] private GameObject _weaponPanel;

        private IHealth _health;
        private TMP_Text _armorText;
        private TMP_Text _weaponText;

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

            _armorText = _armorPanel.GetComponentInChildren<TMP_Text>();
            _weaponText = _weaponPanel.GetComponentInChildren<TMP_Text>();
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

        private void ShowArmorLevel(ItemInfo itemInfo)
        {
            if (itemInfo != null)
            {
                _armorPanel.SetActive(true);
                _armorText.text = itemInfo.Level.ToString();
            }
            else
                _armorPanel.SetActive(false);
        }

        private void ShowWeaponLevel(ItemInfo itemInfo)
        {
            if (itemInfo != null)
            {
                _weaponPanel.SetActive(true);
                _weaponText.text = itemInfo.Level.ToString();
            }
            else
                _weaponPanel.SetActive(false);
        }

        private void UpdateArmorBar() =>
            _armorBar.SetValue(_health.CurrentArmor, _health.MaxArmor);

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
    }
}