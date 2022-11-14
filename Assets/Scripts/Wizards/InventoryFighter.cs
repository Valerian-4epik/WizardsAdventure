using System;
using System.Collections.Generic;
using Enemy;
using UI;
using UnityEngine;
using Wizards;

public class InventoryFighter : MonoBehaviour
{
    [SerializeField] private List<ItemInfo> _allItems;
    [SerializeField] private Transform _handle;
    [SerializeField] private Transform _head;
    [SerializeField] private Attack _attack; //test
    [SerializeField] private CheckAttackRange _attackRange;
    [SerializeField] private ItemInfo _weapon;
    [SerializeField] private ItemInfo _armor;

    private GameObject _weaponObject;
    private GameObject _armorObject;

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

    private void OnEnable() //test
    {
        SetupWeapon();
    }

    public void SetWeapon(List<string> itemsID)
    {
        foreach (var id in itemsID)
        {
            foreach (var item in _allItems)
            {
                if (id == item.ID)
                {
                    if (item.ItemType == ItemType.Armor)
                        _armor = item;
                    else
                        _weapon = item;
                }
            }
        }

        SetupWeapon();
    }

    public void SetWeapon(UIItem uiItem)
    {
        var item = uiItem as UIInventoryItem;

        if (item.Item.ItemType == ItemType.Armor)
        {
            if (_armor == null)
            {
                _armor = item.Item;
                ShowArmor(item.Item);
                ArmorDressed?.Invoke(_armor);
                Refresh(item);
            }
        }
        else
        {
            if (_weapon == null)
            {
                _weapon = item.Item;
                ShowWeapon(item.Item);
                WeaponDressed?.Invoke(_weapon);
                Refresh(item);
            }
        }
    }

    public void ReturnItems(UIInventory shopInterface)
    {
        if (_armor != null)
        {
            shopInterface.SetupItem(_armor);
            _armor = null;
            Destroy(_armorObject);
            ArmorDressed?.Invoke(_armor);
        }

        if (_weapon != null)
        {
            shopInterface.SetupItem(_weapon);
            _weapon = null;
            Destroy(_weaponObject);
            WeaponDressed?.Invoke(_weapon);
        }
    }

    public List<string> GetItemsID()
    {
        var listID = new List<string>();

        if (_weapon != null)
            listID.Add(_weapon.ID);

        if (_armor != null)
            listID.Add(_armor.ID);

        return listID;
    }

    private void SetupWeapon()
    {
        if (_weapon != null)
        {
            ShowWeapon(_weapon);
            _attack.Weapon = new Weapon(transform, _weapon);
            _attackRange.ChangeAttackRange(_weapon.AttackRange);
        }

        if (_armor != null)
            ShowArmor(_armor);
    }

    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();

    private void ShowWeapon(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            var item = Instantiate(itemInfo.Prefab, _handle.position, Quaternion.identity);
            _weaponObject = item.gameObject;
            item.gameObject.transform.SetParent(_handle);
        }
    }

    private void ShowArmor(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            var item = Instantiate(itemInfo.Prefab, _head.position, Quaternion.identity);
            _armorObject = item.gameObject;
            item.gameObject.transform.SetParent(_head);
        }
    }
}