using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private UIInventorySlot[] _slots;
        [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();

        private RaycastDetecter _raycastDetecter;
        
        public UIInventorySlot[] Slots => _slots;
        
        public event Action Fight;
        
        private void Awake()
        {
            var slots = GetComponentsInChildren<UIInventorySlot>();
            _slots = slots;

            foreach (var slot in _slots)
            {
                slot.Refresh();
            }
            
            SetRaycastDetecter();
        }

        public void OnFight() => 
            Fight?.Invoke();

        public void BuyItem(ItemInfo item)
        {
            if (GetEmptySlots().Length != 0)
            {
                UIInventorySlot slot = GetEmptySlots()[0];
                slot.SetItem(item);
            }
        }

        public UIInventorySlot[] GetFullSlots()
        {
            var emptySlot = from slot in _slots where slot.IsFull select slot;
            return emptySlot.ToArray();
        }

        public void Merge(UIInventorySlot fromSlot, UIInventorySlot toSlot)
        {
            if (fromSlot.InventoryItem.Item.Level == toSlot.InventoryItem.Item.Level)
            {
                foreach (var item in _items)
                {
                    if (toSlot.InventoryItem.Item.Level+1 == item.Level)
                    {
                        toSlot.SetItem(item);
                        fromSlot.Refresh();
                        return;
                    }
                }
            }
        }

        private void SetRaycastDetecter()
        {
            if (Camera.main != null)
                _raycastDetecter = Camera.main.gameObject.GetComponent<RaycastDetecter>();
            _raycastDetecter.SetShopInterface(this);
        }
            
        private UIInventorySlot[] GetEmptySlots()
        {
            var emptySlot = from slot in _slots where !slot.IsFull select slot;
            return emptySlot.ToArray();
        }
    }
}