using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardCell : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private TMP_Text _level;

    public void FillCell(Sprite icon, int amount, int level)
    {
        SetImage(icon);
        
        if (_amount != null)
            SetAmount(amount);
        if (_level != null)
            SetLevel(level);
    }

    private void SetImage(Sprite sprite) => _image.sprite = sprite;
    private void SetAmount(int amount) => _amount.text = amount.ToString();
    private void SetLevel(int level) => _level.text = level.ToString();
}