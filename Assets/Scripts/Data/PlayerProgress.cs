using System;
using System.Collections.Generic;
using Data.LevelData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    public class PlayerProgress : MonoBehaviour
    {
        private const int NEW_GAME_MONEY = 30;

        private string _leaderboardName = "LevelValue";
        private bool _isNewGame = true;
        private int _allmoney;
        private int _currentMoney;
        private int _currentLevel = 0;
        private List<string> _itemsInShop = new List<string>();
        private Dictionary<int, List<string>> _itemsInSquad = new Dictionary<int, List<string>>();
        private RewardLevelData _rewardLevelData;
        private int _countNumberSaveMoney;
        private bool _isTutorialStart = true;
        private int _additionalyHP;
        private float _additionalyAttackSpeed;
        private int _currentHp;
        private float _currentAttackSpeed;

        public int PlayerWizardAmount { get; set; }

        public float AdditionalyAttackSpeed => _additionalyAttackSpeed;
        public int AdditionalyHp => _additionalyHP;

        public event Action MoneyChanged;
        public event Action<int, float> StatsChanged;

        private void Awake()
        {
            _rewardLevelData = new RewardLevelData();
            _additionalyAttackSpeed = LoadAdditionalAttackSpeed();
            _additionalyHP = LoadAdditionalHP();
        }

        public void SaveAdditionalHP()
        {
            _additionalyHP = LoadAdditionalHP() + _currentHp;
            ES3.Save("additionalHP", _additionalyHP, "AdditionalHP.es3");
        }

        public int LoadAdditionalHP() => _additionalyHP = ES3.Load("additionalHP", "AdditionalHP.es3", _additionalyHP);

        public void SaveAdditionalAttackSpeed()
        {
            _additionalyAttackSpeed = LoadAdditionalAttackSpeed() + _currentAttackSpeed;
            ES3.Save("additionalAttackSpeed", _additionalyAttackSpeed, "AdditionalAttackSpeed.es3");
        }

        public float LoadAdditionalAttackSpeed() => _additionalyAttackSpeed =
            ES3.Load("additionalAttackSpeed", "AdditionalAttackSpeed.es3", _additionalyAttackSpeed);

        public void AddCurrentHP(int value)
        {
            _currentHp += value;
            StatsChanged?.Invoke(GetAdditionalHp(), GetAdditionalAttackSpeed());
        }

        public void AddCurrentAttackSpeed(float value)
        {
            _currentAttackSpeed += value;
            StatsChanged?.Invoke(GetAdditionalHp(), GetAdditionalAttackSpeed());
        }

        public void SaveAllMoney(int value)
        {
            _allmoney = value;
            ES3.Save("allMoney", _allmoney, "AllMoney.es3");
        }

        public int LoadAllMoney()
        {
            if (GetCountNumber() == 0)
            {
                _allmoney = ES3.Load("allMoney", "AllMoney.es3", NEW_GAME_MONEY);
                SaveCountNumber();
                return _allmoney;
            }
            else
            {
                _allmoney = ES3.Load("allMoney", "AllMoney.es3", NEW_GAME_MONEY);
                return _allmoney;
            }
        }

        public void SaveTutorialStartState(bool value)
        {
            _isTutorialStart = value;
            ES3.Save("tutorialState", _isTutorialStart, "tutorialState.es3");
        }

        public bool GetStartTutorialInfo() =>
            _isTutorialStart = ES3.Load("tutorialState", "tutorialState.es3", _isTutorialStart);

        public void SwitchMoney()
        {
            _allmoney = LoadCurrentMoney();
            SaveAllMoney(_allmoney);
        }

        public void SaveLevel(int level)
        {
            _currentLevel = level;
            ES3.Save("currentLevel", _currentLevel, "CurrentLevel.es3");
            
#if !UNITY_WEBGL || UNITY_EDITOR
            return;
#endif
            SetLeaderboardValue(_leaderboardName, _currentLevel);
        }


        public int GetLevel() => _currentLevel = ES3.Load("currentLevel", "CurrentLevel.es3", _currentLevel);

        public void SaveCurrentItems(List<string> items)
        {
            if (items != null)
            {
                FillItemList(items);
            }

            ES3.Save("myItemsList", _itemsInShop, "MyItemsList.es3");
        }

        public List<string> GetItems()
        {
            _itemsInShop = ES3.Load("myItemsList", "MyItemsList.es3", _itemsInShop);
            return _itemsInShop;
        }

        public void AddItemToCurrentItems(ItemInfo item)
        {
            _itemsInShop = GetItems();
            _itemsInShop.Add(item.ID);
            ES3.Save("myItemsList", _itemsInShop, "MyItemsList.es3");
        }

        public void SavePlayerWizardsAmount() =>
            ES3.Save("mySquad", PlayerWizardAmount, "Squad.es3");


        public int GetPLayerWizardAmount() => PlayerWizardAmount = ES3.Load("mySquad", "Squad.es3", PlayerWizardAmount);

        public void SaveSquadItems(Dictionary<int, List<string>> itemIDs)
        {
            _itemsInSquad = itemIDs;
            ES3.Save("myItemDictionaryInSquad", _itemsInSquad, "MyItemDictionaryInSquad.es3");
        }

        public Dictionary<int, List<string>> LoadSquadItems()
        {
            _itemsInSquad = ES3.Load("myItemDictionaryInSquad", "MyItemDictionaryInSquad.es3", _itemsInSquad);
            return _itemsInSquad;
        }

        public void SaveCurrentMoney(int value)
        {
            _currentMoney = value;
            ES3.Save("myMoney", _currentMoney, "MyMoney.es3");
            MoneyChanged?.Invoke();
        }

        public int LoadCurrentMoney()
        {
            _currentMoney = ES3.Load("myMoney", "MyMoney.es3", _currentMoney);
            return _currentMoney;
        }

        public void AddReward()
        {
            var currentLevelNumber = ES3.Load("currentLevelIndex", "CurrentLevel.es3", _currentLevel);
            SaveAllMoney(LoadAllMoney() + _rewardLevelData.Rewards[currentLevelNumber]);
        }

        public int GetRewardAmount()
        {
            var currentLevel = GetLevel();
            return _rewardLevelData.Rewards[currentLevel];
        }

        public int GetAdditionalHp() => LoadAdditionalHP() + _currentHp;
        public float GetAdditionalAttackSpeed() => LoadAdditionalAttackSpeed() + _currentAttackSpeed;

        private void SavePlayerWizardsAmount(int value) =>
            ES3.Save("mySquad", value, "Squad.es3");

        private void FillItemList(List<string> items)
        {
            _itemsInShop = new List<string>();

            foreach (var item in items)
            {
                _itemsInShop.Add(item);
            }
        }

        private void SaveCountNumber()
        {
            _countNumberSaveMoney++;
            ES3.Save("countNumber", _countNumberSaveMoney, "countNumber.es3");
        }

        private int GetCountNumber()
        {
            var countNumber = ES3.Load("countNumber", "countNumber.es", _countNumberSaveMoney);
            return countNumber;
        }

        private void SetLeaderboardValue(string leaderboardName, int value)
        {
            Agava.YandexGames.Leaderboard.SetScore(leaderboardName, value, OnSucces, OnError);
            
            void OnSucces()
            {
                return;
            }

            void OnError(string error)
            {
                return;
            }
        }
    }
}