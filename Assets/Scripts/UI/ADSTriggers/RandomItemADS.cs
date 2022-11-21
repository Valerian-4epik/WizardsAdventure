using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomItemADS : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _doneImage;
    [SerializeField] private GameObject _description;

    private ItemInfo _item;
    
    public ItemInfo Item => _item;

    public event Action ButtonClick;

    public void SetItem(ItemInfo item)
    {
        _item = item;
        SetImage(_item.Icon);
    }

    private void SetImage(Sprite sprite) => 
        _image.sprite = sprite;

    public void BlockButton()
    {
        _button.enabled = false;
        _image.gameObject.SetActive(false);
        _description.SetActive(false);
        _doneImage.SetActive(true);
    }
}