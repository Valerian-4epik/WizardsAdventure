using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Wizards;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Image _damageTypeIcon;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _attackSpeed;
    [SerializeField] private Sprite _magicAttack;

    public void FillPanel(ItemInfo itemInfo)
    {
        if (itemInfo.Armor == 0)
        {
            if (itemInfo.AttackType == AttackType.MeleeAttack)
            {
                FillTextInfo(itemInfo);
            }
            else if (itemInfo.AttackType == AttackType.RangeAttack)
            {
                _damageTypeIcon.sprite = _magicAttack;
                FillTextInfo(itemInfo);
            }
        }
    }

    private void FillTextInfo(ItemInfo item)
    {
        _damage.text = ($" - {item.Damage.ToString()}");
        _attackSpeed.text = ($" - {item.AttackSpeed.ToString()}");
    }
}