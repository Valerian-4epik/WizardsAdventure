using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Wizards;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private UIInventory _inventory;
    [SerializeField] private Image _damageTypeIcon;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _attackSpeed;
    [SerializeField] private Sprite _magicAttackIcon;
    [SerializeField] private Sprite _meleeAttackIcon;
    [SerializeField] private Image _currentItem;
    [SerializeField] private Image _nextItem;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _nextLevel;
    [SerializeField] private Image _currentBack;
    [SerializeField] private Image _nextBack;
    [SerializeField] private GameObject _attackPanel;
    [SerializeField] private GameObject _armorPanel;
    [SerializeField] private TMP_Text _armor;

    private ItemInfo _item;

    public ItemInfo Item => _item;

    public void FillPanel(ItemInfo itemInfo)
    {
        _item = itemInfo;

        if (itemInfo.Armor == 0)
        {
            if (itemInfo.AttackType == AttackType.MeleeAttack)
            {
                SwithPanels();
                _damageTypeIcon.sprite = _meleeAttackIcon;
                FillTextInfo(itemInfo);
            }
            else if (itemInfo.AttackType == AttackType.RangeAttack)
            {
                SwithPanels();
                _damageTypeIcon.sprite = _magicAttackIcon;
                FillTextInfo(itemInfo);
            }
        }
        else
        {
            _attackPanel.SetActive(false);
            _armorPanel.SetActive(true);
            _armor.text = ($"- {itemInfo.Armor.ToString()}");
        }

        FillSlotsItemInfo(itemInfo);
    }

    private void SwithPanels()
    {
        _armorPanel.SetActive(false);
        _attackPanel.SetActive(true);
    }

    private void FillSlotsItemInfo(ItemInfo itemInfo)
    {
        _currentItem.sprite = itemInfo.Icon;
        _currentLevel.text = itemInfo.Level.ToString();
        _currentBack.sprite = itemInfo.Back;
        var nextItem = GetNextItemInfo(itemInfo);
        _nextItem.sprite = nextItem.Icon;
        _nextLevel.text = nextItem.Level.ToString();
        _nextBack.sprite = nextItem.Back;
    }

    private ItemInfo GetNextItemInfo(ItemInfo itemInfo) => _inventory.GetNextItem(itemInfo);

    private void FillTextInfo(ItemInfo item)
    {
        _damage.text = ($" - {item.Damage.ToString()}");
        _attackSpeed.text = ($" - {item.AttackSpeed.ToString()}");
    }
}