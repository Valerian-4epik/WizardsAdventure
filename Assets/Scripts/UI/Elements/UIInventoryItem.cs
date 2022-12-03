using System;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _infoButton;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private InfoPanel _infoPanel;


    private bool _isFull;

    public ItemInfo Item { get; private set; }

    public override void Start()
    {
        base.Start();
        _infoButton.onClick.AddListener(OpenInfoPanel);
    }

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
        _infoPanel.gameObject.SetActive(false);
        _infoButton.gameObject.SetActive(false);
        _canvasGroup.alpha = 0;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        _canvasGroup.alpha = 1;
        _isFull = gameObject.GetComponentInParent<UIInventorySlot>().IsFull;
        if (_isFull)
            _infoButton.gameObject.SetActive(true);
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

    public void ActivateInfoButton()
    {
        _infoButton.gameObject.SetActive(true);
        _infoButton.onClick.AddListener(OpenInfoPanel);
    }

    private void OpenInfoPanel()
    {
        if (_infoPanel.gameObject.activeSelf && _infoPanel.Item != Item)
        {
            _infoPanel.gameObject.SetActive(false);
            _infoPanel.gameObject.SetActive(true);
            SpendItemInfo(Item);
        }
        else if(_infoPanel.gameObject.activeSelf && _infoPanel.Item == Item)
            _infoPanel.gameObject.SetActive(false);
        else
        {
            _infoPanel.gameObject.SetActive(true);
            SpendItemInfo(Item);
        }
    }

    private void SpendItemInfo(ItemInfo item) => _infoPanel.FillPanel(item);
}