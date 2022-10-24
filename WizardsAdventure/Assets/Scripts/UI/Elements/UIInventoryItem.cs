using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TMP_Text _levelText;
    
    public ItemInfo item { get; private set; }
    
    public void Refresh(UIInventorySlot slot)
    {
        if (!slot.IsFull)
        {
            Cleanup();
            return;
        }

        item = slot.InventoryItem.item;
        _imageIcon.sprite = item.icon;
        _levelText.text = item.level.ToString();

        var levelText = slot.InventoryItem.item.level > 0;
        
        if (!levelText)
            _levelText.GetComponentInParent<Image>().enabled = false;
    }

    public void SetItem(ItemInfo item)
    {
        this.item = item;
        _imageIcon.gameObject.SetActive(true);
        _levelText.gameObject.SetActive(true);
    }

    public void Cleanup()
    {
        _imageIcon.gameObject.SetActive(false);
        _levelText.gameObject.SetActive(false);
    }
}
