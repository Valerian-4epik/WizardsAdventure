using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Data
{
    public class PlayerProgress : MonoBehaviour
    {
        private int _money;
        private int _currentLevel;
        private List<string> _itemsInShop = new List<string>();
        private Dictionary<int, List<string>> _itemsInSquad = new Dictionary<int, List<string>>();

        public int PlayerWizardsAmount { get; set; }
        
        public void SaveCurrentSceneNumber()
        {
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            ES3.Save("currentLevelIndex", _currentLevel, "CurrentLevel.es3");
            Debug.Log("SaveLevelIndex"); 
            Debug.Log(_currentLevel);
        }

        public int GetNextScene()
        {
            _currentLevel = ES3.Load("currentLevelIndex", "CurrentLevel.es3", _currentLevel);
            Debug.Log("LoadLevelIndex");
            Debug.Log(_currentLevel);
            return _currentLevel + 1;
        }

        public void SaveCurrentItems(List<string> items)
        {
            _itemsInShop = new List<string>();
            
            foreach (var item in items)
            {
                _itemsInShop.Add(item);
            }
            
            ES3.Save("myItemsList", _itemsInShop, "MyItemsList.es3");
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

        public Dictionary<int, List<string>> LoadSquadItems()
        { 
            _itemsInSquad = ES3.Load("myItemDictionaryInSquad", "MyItemDictionaryInSquad.es3", _itemsInSquad);
            return _itemsInSquad;
        }
    }
}