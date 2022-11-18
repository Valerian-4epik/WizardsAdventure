using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    public class PlayerProgress : MonoBehaviour
    {
        private bool _isNewGame = true;
        private int _money = 300;
        private int _currentLevel;
        private List<string> _itemsInShop = new List<string>();
        private Dictionary<int, List<string>> _itemsInSquad = new Dictionary<int, List<string>>();
        private RewardLevelData _rewardLevelData;

        public int PlayerWizardsAmount { get; set; }

        public event Action MoneyChanged;

        private void Awake() =>
            _rewardLevelData = new RewardLevelData();

        public void UpdateStatistics()
        {
            _money = 300;
            PlayerWizardsAmount = 0;
            _itemsInShop = new List<string>();
            _itemsInSquad = new Dictionary<int, List<string>>();

            SaveGameStata(true);
            SaveMoney(_money);
            SaveCurrentItems(_itemsInShop);
            SaveSquadItems(_itemsInSquad);
            SavePlayerWizardsAmount(PlayerWizardsAmount);
        }

        public void SaveGameStata(bool value)
        {
            _isNewGame = value;
            ES3.Save("gameState", _isNewGame, "GameState.es3");
        }

        public bool GetGameState()
        {
            _isNewGame = ES3.Load("gameState", "GameState.es3", _isNewGame);
            return _isNewGame;
        }

        public void SaveCurrentSceneNumber()
        {
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            ES3.Save("currentLevelIndex", _currentLevel, "CurrentLevel.es3");
        }


        public int GetCurrentScene() =>
            _currentLevel = SceneManager.GetActiveScene().buildIndex;

        public int GetNextScene()
        {
            _currentLevel = ES3.Load("currentLevelIndex", "CurrentLevel.es3", _currentLevel);
            return _currentLevel + 1;
        }

        public void SaveCurrentItems(List<string> items)
        {
            if (items != null)
            {
                FillItemList(items);
            }

            ES3.Save("myItemsList", _itemsInShop, "MyItemsList.es3");
        }

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

        public List<string> GetItems()
        {
            _itemsInShop = ES3.Load("myItemsList", "MyItemsList.es3", _itemsInShop);
            return _itemsInShop;
        }

        public void SaveSquadItems(Dictionary<int, List<string>> itemIDs)
        {
            _itemsInSquad = itemIDs;
            ES3.Save("myItemDictionaryInSquad", _itemsInSquad, "MyItemDictionaryInSquad.es3");
        }

        public void SaveMoney(int value)
        {
            _money = value;
            ES3.Save("myMoney", _money, "MyMoney.es3");
            MoneyChanged?.Invoke();
        }

        public int LoadCurrentMoney()
        {
            _money = ES3.Load("myMoney", "MyMoney.es3", _money);
            return _money;
        }

        public Dictionary<int, List<string>> LoadSquadItems()
        {
            _itemsInSquad = ES3.Load("myItemDictionaryInSquad", "MyItemDictionaryInSquad.es3", _itemsInSquad);
            return _itemsInSquad;
        }

        public void GetReward()
        {
            var currentLevelNumber = ES3.Load("currentLevelIndex", "CurrentLevel.es3", _currentLevel);
            _money = LoadCurrentMoney();
            Debug.Log(_rewardLevelData.Rewards[currentLevelNumber]);
            _money += _rewardLevelData.Rewards[currentLevelNumber];
            SaveMoney(_money);
        }
    }
}