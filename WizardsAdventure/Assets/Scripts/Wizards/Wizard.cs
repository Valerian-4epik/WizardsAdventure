using Enemy;
using UnityEngine;

namespace Wizards
{
    public class Wizard : MonoBehaviour
    {
        [SerializeField] private InventoryFighter _inventory;
        [SerializeField] private CheckAttackRange _checkAttackRange;
        [SerializeField] private Attack _attack;

        private Weapon _weapon;
        private Armor _armor;

        private void OnEnable()
        {
            _inventory.WeaponDressed += SetWeapon;
            _inventory.ArmorDressed += SetArmor;
        }

        private void OnDestroy()
        {
            _inventory.WeaponDressed -= SetWeapon;
            _inventory.ArmorDressed -= SetArmor;
        }

        private void SetupAttackRange(ItemInfo item) => 
            _checkAttackRange.ChangeAttackRange(item.AttackRange);

        private void SetupAttackValue(ItemInfo item)
        {
            _attack.Weapon ;
        }

        private void SetWeapon(ItemInfo item) => 
            _weapon = new Weapon(transform, item);

        private void SetArmor(ItemInfo item) =>
            _armor = new Armor(item);
    }
}