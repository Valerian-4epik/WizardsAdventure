using System;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using Wizards;

public class InventoryFighter : MonoBehaviour
{
    private ItemInfo _weapon;
    private ItemInfo _armor;

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

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

    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();
}