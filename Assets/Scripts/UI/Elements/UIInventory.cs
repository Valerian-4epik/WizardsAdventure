using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private UIInventorySlot[] _slots;
        [SerializeField] private List<ItemInfo> _itemsData = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItems = new List<ItemInfo>();
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _currentMoney;
        [SerializeField] private RandomItemADS _adsTrigger;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _successfulBuy;
        [SerializeField] private AudioClip _successfulMerge;
        [SerializeField] private AudioClip _worngFxSound;
        [SerializeField] private List<Button> _buttonsForTutor;
        [SerializeField] private Button _branchButton;
        [SerializeField] private StatsInfo _statsInfo;

        private RaycastDetecter _raycastDetecter;
        private PlayerProgress _playerProgress;

        public PlayerProgress PlayerProgress => _playerProgress;
        public UIInventorySlot[] Slots => _slots;
        public Button BranchButton => _branchButton;
        public CanvasGroup CanvasGroup => _canvasGroup;

        public event Action Fight;

        private void Awake()
        {
            var slots = GetComponentsInChildren<UIInventorySlot>();
            _slots = slots;

            foreach (var slot in _slots)
            {
                slot.Refresh();
            }

            SetRaycastDetecter();

            _adsTrigger.SetItem(SetupRandomItem());
        }

        public void OnFight()
        {
            _playerProgress.SaveCurrentItems(ConvertListItemsToItemID(ConvertListSlotsToItem()));
            _playerProgress.SwitchMoney();
            _playerProgress.SavePlayerWizardsAmount();
            Fight?.Invoke();
        }

        public void SetupItem(ItemInfo item)
        {
            if (GetEmptySlots().Length != 0)
                FillSlot(item);
            else
                ReturnMoneyForItem(item);
        }

        public void BuyItem(ItemInfo item)
        {
            if (_playerProgress.LoadCurrentMoney() >= item.Price && GetEmptySlots().Length != 0)
            {
                PLaySoundFx(_successfulBuy);
                _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() - item.Price);
                FillSlot(item);
            }
            else
            {
                PLaySoundFx(_worngFxSound);
                Debug.Log("Недостаточно денег");
            }
        }

        public void Merge(UIInventorySlot fromSlot, UIInventorySlot toSlot)
        {
            if (GetItemLevel(fromSlot) == GetItemLevel(toSlot) &&
                GetItemTypeOfObject(fromSlot) == GetItemTypeOfObject(toSlot))
            {
                foreach (var item in _itemsData.Where(item =>
                             toSlot.InventoryItem.Item.Level + 1 == item.Level &&
                             GetItemTypeOfObject(toSlot) == item.TypeOfObject))
                {
                    toSlot.SetItem(item);
                    PLaySoundFx(_successfulMerge);
                    fromSlot.Refresh();
                    return;
                }
            }
            else
            {
                PLaySoundFx(_worngFxSound);
                Debug.Log("wrong");
            }
        }

        public void ActivateBranchButtonForTutor()
        {
            _canvasGroup.interactable = true;
            foreach (var button in _buttonsForTutor)
            {
                button.interactable = false;
            }
        }

        public void SetPlayerProgress(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _playerProgress.MoneyChanged += ShowMoney;
            _playerProgress.SaveCurrentMoney(_playerProgress.LoadAllMoney());
            LoadCurrentItems();
            ShowMoney();
            ActivateStatPanel(_playerProgress.GetAdditionalHp(), _playerProgress.GetAdditionalAttackSpeed());
            _playerProgress.StatsChanged += ActivateStatPanel;
        }

        private void ActivateStatPanel(int hp, float attackSpeed) => _statsInfo.ActivateStatsPanel(hp, attackSpeed);

        public void ShowInventory()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void PlayGoldBuy() => PLaySoundFx(_successfulBuy);

        public ItemInfo GetNextItem(ItemInfo currentItem)
        {
            foreach (var item in _itemsData)
            {
                if (item.Level == currentItem.Level + 1 && item.TypeOfObject == currentItem.TypeOfObject)
                    return item;
            }

            return null;
        }

        private void ReturnMoneyForItem(ItemInfo item) => 
            _playerProgress.SaveCurrentMoney(_playerProgress.LoadCurrentMoney() + item.Price);

        private void PLaySoundFx(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;

            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }

        private ItemInfo SetupRandomItem()
        {
            var number = Random.Range(0, _rewardItems.Count);
            return _rewardItems[number];
        }

        private void ShowMoney() =>
            _currentMoney.text = _playerProgress.LoadCurrentMoney().ToString();

        private void LoadCurrentItems()
        {
            if (GetItemList() != null)
            {
                foreach (var item in GetItemList())
                {
                    FillSlot(item);
                }
            }
        }

        private void FillSlot(ItemInfo item)
        {
            if (GetEmptySlots().Length != 0)
            {
                UIInventorySlot slot = GetEmptySlots()[0];
                slot.SetItem(item);
            }
            else
            {
                Debug.Log("Wrong");
                ReturnMoneyForItem(item);
                PLaySoundFx(_worngFxSound);
            }
        }

        private void SetRaycastDetecter()
        {
            if (Camera.main != null)
            {
                _raycastDetecter = Camera.main.gameObject.GetComponent<RaycastDetecter>();
                _raycastDetecter.SetShopInterface(this);
            }
        }

        private List<ItemInfo> GetItemList() =>
            (from itemID in _playerProgress.GetItems() from item in _itemsData where item.ID == itemID select item)
            .ToList();

        private TypeOfObject GetItemTypeOfObject(UIInventorySlot fromSlot) =>
            fromSlot.InventoryItem.Item.TypeOfObject;

        private int GetItemLevel(UIInventorySlot fromSlot) =>
            fromSlot.InventoryItem.Item.Level;

        private UIInventorySlot[] GetEmptySlots() =>
            (from slot in _slots where !slot.IsFull select slot).ToArray();

        private List<UIInventorySlot> GetFullSlots() =>
            (from slot in _slots where slot.IsFull select slot).ToList();

        private List<ItemInfo> ConvertListSlotsToItem() =>
            (GetFullSlots().Select(slot => slot.InventoryItem.Item).ToList());

        private List<string> ConvertListItemsToItemID(List<ItemInfo> items) =>
            (items.Select(item => item.ID)).ToList();
    }
}