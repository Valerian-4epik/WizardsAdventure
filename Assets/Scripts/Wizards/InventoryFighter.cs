using System;
using System.Collections.Generic;
using Enemy;
using UI;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Wizards;

public class InventoryFighter : MonoBehaviour
{
    [SerializeField] private List<ItemInfo> _allItems;
    [SerializeField] private Transform _handleRight;
    [SerializeField] private Transform _handleLeft;
    [SerializeField] private Transform _head;
    [SerializeField] private Attack _attack; //test
    [SerializeField] private CheckAttackRange _attackRange;
    [SerializeField] private Health _health;
    [SerializeField] private ItemInfo _weapon;
    [SerializeField] private ItemInfo _armor;

    private AudioPlayerForWizard _audioPlayer;
    private GameObject _weaponObject;
    private GameObject _armorObject;
    private WizardAnimator _animator;
    private Vector3 _currentScale;

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

    private void OnEnable() //test
    {
        _audioPlayer = GetComponentInChildren<AudioPlayerForWizard>();
        _animator = GetComponent<WizardAnimator>();
        _currentScale = gameObject.transform.localScale;
        SetupWeapon();
    }

    public void SetWeapon(List<string> itemsID)
    {
        foreach (var id in itemsID)
        {
            foreach (var item in _allItems)
            {
                if (id == item.ID)
                {
                    if (item.ItemType == ItemType.Armor)
                        _armor = item;
                    else
                        _weapon = item;
                }
            }
        }

        SetupWeapon();
    }

    public void SetWeapon(UIItem uiItem)
    {
        var item = uiItem as UIInventoryItem;

        if (item.Item.ItemType == ItemType.Armor)
        {
            if (_armor == null)
            {
                _armor = item.Item;
                DressArmor(item);
                Refresh(item);
            }
            else
            {
                var currentArmor = _armor;
                Destroy(_armorObject);
                _armor = item.Item;
                DressArmor(item);
                Refresh(item);
                SwitchItem(item, currentArmor);
            }
        }
        else
        {
            if (_weapon == null)
            {
                _weapon = item.Item;
                DressWeapon(item);
                Refresh(item);
            }
            else
            {
                var currentWeapon = _weapon;
                Destroy(_weaponObject);
                _weapon = item.Item;
                DressWeapon(item);
                Refresh(item);
                SwitchItem(item, currentWeapon);
            }
        }
    }

    private void DressArmor(UIInventoryItem item)
    {
        ShowArmor(item.Item);
        PlayTakeWeaponAnimation();
        ArmorDressed?.Invoke(_armor);
    }

    private void DressWeapon(UIInventoryItem item)
    {
        ShowWeapon(item.Item);
        PlayTakeWeaponAnimation();
        WeaponDressed?.Invoke(_weapon);
    }

    public void ReturnItems(UIInventory shopInterface)
    {
        if (_armor != null || _weapon != null)
        {
            if (_armor != null)
            {
                shopInterface.SetupItem(_armor);
                _armor = null;
                _health.RefreshValue();
                Destroy(_armorObject);
                ArmorDressed?.Invoke(_armor);
            }

            if (_weapon != null)
            {
                shopInterface.SetupItem(_weapon);
                _weapon = null;

                Destroy(_weaponObject);
                WeaponDressed?.Invoke(_weapon);
            }

            PlayReturnWeaponAnimation();
        }
    }

    public List<string> GetItemsID()
    {
        var listID = new List<string>();

        if (_weapon != null)
            listID.Add(_weapon.ID);

        if (_armor != null)
            listID.Add(_armor.ID);

        return listID;
    }

    private void PlayTakeWeaponAnimation() => _animator.PlayTakeWeapon();
    private void PlayReturnWeaponAnimation() => _animator.PlayReturnWeapon();
    private void OnStartRejoices() => _audioPlayer.PlayRejoicedEmotion();
    private void OnStartSad() => _audioPlayer.PlaySadEmotion();

    private void SetupWeapon()
    {
        if (_weapon != null)
        {
            ShowWeapon(_weapon);
            _attack.Weapon = new Weapon(transform, _weapon);
            _attackRange.ChangeAttackRange(_weapon.AttackRange);
            WeaponDressed.Invoke(_weapon);
        }

        if (_armor != null)
        {
            ShowArmor(_armor);
            ArmorDressed.Invoke(_armor);
        }
        else
            _health.AssignArmor(0, 0);
    }

    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();
    
    private void SwitchItem(UIInventoryItem item, ItemInfo itemInfo) =>
        item.GetComponentInParent<UIInventorySlot>().SetItem(itemInfo);

    private void ShowWeapon(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            var item = SetupItem(itemInfo, _handleRight);

            if (itemInfo.AttackType == AttackType.RangeAttack)
                _attack.SetProjectileShootPoint(item.gameObject.GetComponentInChildren<ProjectileShootPoint>()
                    .gameObject.transform);

            _weaponObject = item.gameObject;
        }
    }

    private void ShowArmor(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            if (itemInfo.TypeOfObject == TypeOfObject.Hat)
                SetupArmorItem(itemInfo, _head);
            else if (itemInfo.TypeOfObject == TypeOfObject.Shield)
                SetupArmorItem(itemInfo, _handleLeft);
            else
                _health.AssignArmor(_armor.Armor, _armor.Level);
        }
    }

    private void SetupArmorItem(ItemInfo itemInfo, Transform point)
    {
        var item = SetupItem(itemInfo, point);

        _armorObject = item.gameObject;
        _health.AssignArmor(_armor.Armor, _armor.Level);
    }

    private GameObject SetupItem(ItemInfo itemInfo, Transform point)
    {
        NormalizeScale();
        var item = Instantiate(itemInfo.Prefab, point.position, Quaternion.identity);
        item.gameObject.transform.SetParent(point);
        SetCurrentScale();
        return item;
    }

    private Vector3 NormalizeScale() => gameObject.transform.localScale = new Vector3(1, 1, 1);
    private void SetCurrentScale() => gameObject.transform.localScale = _currentScale;
}