using System;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using Wizards;

public class InventoryFighter : MonoBehaviour
{
    [SerializeField] private List<ItemInfo> _allItems;
        
    private ItemInfo _weapon;
    private ItemInfo _armor;

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

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
                WeaponDressed?.Invoke(_weapon);
                Refresh(item);
            }
        }
    }

    public void ReturnItems(UIInventory shopInterface)
    {
        if (_armor != null)
        {
            shopInterface.BuyItem(_armor);
            _armor = null;
            ArmorDressed?.Invoke(_armor);
        }

        if (_weapon != null)
        {
            shopInterface.BuyItem(_weapon);
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
}