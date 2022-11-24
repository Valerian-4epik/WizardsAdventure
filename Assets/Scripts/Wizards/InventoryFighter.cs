using System;
using System.Collections.Generic;
using Enemy;
using UI;
using UnityEngine;
using Wizards;

public class InventoryFighter : MonoBehaviour
{
    [SerializeField] private List<ItemInfo> _allItems;
    [SerializeField] private Transform _handle;
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

    public event Action<ItemInfo> WeaponDressed;
    public event Action<ItemInfo> ArmorDressed;

    private void OnEnable() //test
    {
        _audioPlayer = gameObject.GetComponentInChildren<AudioPlayerForWizard>();
        _animator = gameObject.GetComponent<WizardAnimator>();
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
                ShowArmor(item.Item);
                PlayAnimatiom();
                ArmorDressed?.Invoke(_armor);
                Refresh(item);
            }
        }
        else
        {
            if (_weapon == null)
            {
                _weapon = item.Item;
                ShowWeapon(item.Item);
                PlayAnimatiom();
                WeaponDressed?.Invoke(_weapon);
                Refresh(item);
            }
        }
    }

    public void ReturnItems(UIInventory shopInterface)
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

    private void PlayAnimatiom() => _animator.PlayTakeWeapon();

    private void SetupWeapon()
    {
        if (_weapon != null)
        {
            ShowWeapon(_weapon);
            _attack.Weapon = new Weapon(transform, _weapon);
            _attackRange.ChangeAttackRange(_weapon.AttackRange);
        }

        if (_armor != null)
        {
            ShowArmor(_armor);
        }
        else
            _health.AssignArmor(0, 0);
    }

    private void OnStartRejoices() => _audioPlayer.PlayRejoicedEmotion();

    private void Refresh(UIInventoryItem item) =>
        item.GetComponentInParent<UIInventorySlot>().Refresh();

    private void ShowWeapon(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            var item = Instantiate(itemInfo.Prefab, _handle.position, Quaternion.identity);
            
            if (itemInfo.AttackType == AttackType.RangeAttack)
                _attack.SetProjectileShootPoint(item.gameObject.GetComponentInChildren<ProjectileShootPoint>().gameObject.transform);
            
            _weaponObject = item.gameObject;
            item.gameObject.transform.SetParent(_handle);
        }
    }

    private void ShowArmor(ItemInfo itemInfo)
    {
        if (itemInfo != null)
        {
            if (itemInfo.TypeOfObject == TypeOfObject.Hat)
            {
                var item = Instantiate(itemInfo.Prefab, _head.position, Quaternion.identity);
                _armorObject = item.gameObject;
                _health.AssignArmor(_armor.Armor, _armor.Level);
                item.gameObject.transform.SetParent(_head);
            }
            else
                _health.AssignArmor(_armor.Armor, _armor.Level);
        }
    }
}