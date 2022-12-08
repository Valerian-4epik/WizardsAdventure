using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RewardSystem
{
    public class RewardItemGenerator : MonoBehaviour
    {
        [SerializeField] private LevelFinishInterface _levelFinishInterface;
        [SerializeField] private List<ItemInfo> _rewardItemsArc1 = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItemsArc2 = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItemsArc3 = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItemsArc4 = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItemsArc5 = new List<ItemInfo>();
        [SerializeField] private List<ItemInfo> _rewardItemsArc6 = new List<ItemInfo>();
        
        [SerializeField] private RewardCoinsCell _coinsCell;
        [SerializeField] private List<RewardCell> _cells = new List<RewardCell>();

        private List<ItemInfo> _multipliedList = new List<ItemInfo>();
        private int _curretnLevel;
        private PlayerProgress _playerProgress;
        private List<ItemInfo> _winnedItems = new List<ItemInfo>();
        
        private void TakeWinedItems(List<ItemInfo> winnedItems)
        {
            foreach (var winnedItem in _winnedItems)
            {
               winnedItems.Add(winnedItem); 
            }
        }

        public void GetPlayerProgress(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _curretnLevel = _playerProgress.GetLevel();
            FillCells();
            FillCoinCell();
            TakeWinedItems(_levelFinishInterface.RewardItems);
        }

        private ItemInfo GetRandomValue(List<ItemInfo> items)
        {
            var newValue = Random.Range(0, items.Count-1);
            return items[newValue];
        }

        private void FillCells()
        {
            var list = GetArcList();
            foreach (var sell in _cells)
            {
                var item = GetRandomValue(list);
                sell.FillCell(item.Icon, item.Price, item.Level);
                _winnedItems.Add(item);
            }
        }

        private List<ItemInfo> GetArcList()
        {
            if (_curretnLevel < 12)
                return _rewardItemsArc1;
            if (_curretnLevel < 24)
                return _rewardItemsArc2;
            if (_curretnLevel < 36)
                return _rewardItemsArc3;
            if (_curretnLevel < 48)
                return _rewardItemsArc4;
            if (_curretnLevel < 60)
                return _rewardItemsArc5;
            if (_curretnLevel < 72)
                return _rewardItemsArc6;

            return _rewardItemsArc1;
        }

        private void FillCoinCell() => _coinsCell.FillCell(_playerProgress.GetRewardAmount());
    }
}
