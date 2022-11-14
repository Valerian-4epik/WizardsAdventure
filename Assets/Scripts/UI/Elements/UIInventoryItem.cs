using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _levelText;
    
    public ItemInfo Item { get; private set; }
    
    public void Refresh(UIInventorySlot slot)
    {
        if (!slot.IsFull)
        {
            Cleanup();
            return;
        }

        Item = slot.InventoryItem.Item;
        _imageIcon.sprite = Item.Icon;
        _levelText.text = Item.Level.ToString();

        var levelText = slot.InventoryItem.Item.Level > 0;
        
        if (!levelText)
            _levelText.GetComponentInParent<Image>().enabled = false;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        _canvasGroup.alpha = 0;
        Debug.Log("NEw begin drag");
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        _canvasGroup.alpha = 1;
    }
    
    public void SetItem(ItemInfo item)
    {
        Item = item;
        _imageIcon.gameObject.SetActive(true);
        _levelText.gameObject.SetActive(true);
    }

    public void Cleanup()
    {
        _imageIcon.gameObject.SetActive(false);
        _levelText.gameObject.SetActive(false);
    }
}
