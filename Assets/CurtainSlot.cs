using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurtainSlot : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _backgroundForLevelNumber;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Image _itemImage;

    public void FillSlot(Sprite background, Sprite backgrounLevelNumber, ItemInfo item)
    {
        _background.sprite = background;
        _backgroundForLevelNumber.sprite = backgrounLevelNumber;
        _levelText.text = item.Level.ToString();
        _itemImage.sprite = item.Icon;
    }
}
