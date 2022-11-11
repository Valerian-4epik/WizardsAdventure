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
    [SerializeField] private Attack _attack; //test
    [SerializeField] private CheckAttackRange _attackRange;
        
    [SerializeField] private ItemInfo _weapon;
    private ItemInfo _armor;

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

    private void OnEnable() //test
    {
        if (_weapon != null)
        {
            ShowWeapon(_weapon);
            _attack.Weapon = new Weapon(transform, _weapon);
            _attackRange.ChangeAttackRange(_weapon.AttackRange);
        }
        if(_armor != null)
            ShowWeapon(_armor);
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
    }

    public void SetWeapon(UIItem uiItem)
    {
        var item = uiItem as UIInventoryItem;

        if (item.Item.ItemType == ItemType.Armor)
        {
            if (_armor == null)
            {
                _armor = item.Item;
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
            ArmorDressed?.Invoke(_armor);
        }

        if (_weapon != null)
        {
            shopInterface.SetupItem(_weapon);
            _weapon = null;
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

    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();

    private void ShowWeapon(ItemInfo itemInfo)
    {
        var item = Instantiate(itemInfo.Prefab, _handle.position, Quaternion.identity);
        item.gameObject.transform.SetParent(_handle);
        
        // weaponHandle.gameObject.transform.position = _handle.position;
    }
}