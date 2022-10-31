using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _inventoryItem;
    
    private bool _isFull;
    private UIInventory _inventory;

    public bool IsFull => _isFull;
    public UIInventoryItem InventoryItem => _inventoryItem;
    public UIInventorySlot slot { get; private set; }

    public Action SlotStateChanged;
    
    private void Awake()
    {
        _inventoryItem = GetComponentInChildren<UIInventoryItem>();
        _inventory = GetComponentInParent<UIInventory>();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        var fromSlotUI = eventData.pointerDrag.GetComponentInParent<UIInventorySlot>();

        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;

        var otherItemUI =
            eventData.pointerDrag
                .GetComponent<UIInventoryItem>();
        var toSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();

        otherItemTransform.SetParent(fromSlotUI.transform);

        if (toSlotUI == fromSlotUI)
        {
          return;
        }
        
        if (toSlotUI.IsFull)
        {
            _inventory.Merge(fromSlotUI, toSlotUI);
        }
        else
        {
            toSlotUI.SetItem(otherItemUI.Item);
            fromSlotUI._isFull = false;
            fromSlotUI.InventoryItem.Cleanup();
            SlotStateChanged?.Invoke();
        }
    }

    public void SetItem(ItemInfo item)
    {
        _isFull = true;
        _inventoryItem.SetItem(item);
        _inventoryItem.Refresh(this);
        SlotStateChanged?.Invoke();
    }

    public void Refresh()
    {
        _isFull = false;
        _inventoryItem.Refresh(this);
    }
}