using System.Collections.Generic;
using Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RewardSystem
{
    public class RewardItemGenerator : MonoBehaviour
    {
        [SerializeField] private LevelFinishInterface _levelFinishInterface;
        [SerializeField] private List<ItemInfo> _rewardItems = new List<ItemInfo>();
        [SerializeField] private RewardCoinsCell _coinsCell;
        [SerializeField] private List<RewardCell> _cells = new List<RewardCell>();

        private List<ItemInfo> _multipliedList = new List<ItemInfo>();

        private PlayerProgress _playerProgress;
        private List<ItemInfo> _winnedItems = new List<ItemInfo>();

        private void OnEnable() => MultiplieItems();

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
            FillCells();
            FillCoinCell();
            TakeWinedItems(_levelFinishInterface.RewardItems);
        }

        private void MultiplieItems()
        {
            foreach (var item in _rewardItems)
            {
                for (int i = 0; i < 5; i++)
                {
                    _multipliedList.Add(item);
                }
            }
        }

        private int GetRandomValue()
        {
            var newValue = Random.Range(0, _multipliedList.Count);
            return newValue;
        }

        private void FillCells()
        {
            foreach (var sell in _cells)
            {
                var item = _multipliedList[GetRandomValue()];
                sell.FillCell(item.Icon, item.Price, item.Level);
                _winnedItems.Add(item);
            }
        }

        private void FillCoinCell() => _coinsCell.FillCell(_playerProgress.GetRewardAmount());
    }
}
