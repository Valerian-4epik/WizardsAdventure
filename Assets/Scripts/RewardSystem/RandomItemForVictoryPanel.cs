using System.Collections.Generic;
using Agava.YandexGames;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RewardSystem
{
    public class RandomItemForVictoryPanel : MonoBehaviour
    {
        [SerializeField] private LevelFinishInterface _levelFinishInterface;
        [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();
        [SerializeField] private GameObject _freePanel;
        [SerializeField] private GameObject _levelBack;
        [SerializeField] private Image _itemImag;
        [SerializeField] private TMP_Text _levelText;

        private PlayerProgress _playerProgress;
        private Button _button;

        private void OnEnable()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Show);
            _playerProgress = _levelFinishInterface.PlayerProgress;
        }

        private void Show()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            FillCell();
            BlockFreeButton();
#endif
            VideoAd.Show(onRewardedCallback:FillCell, onCloseCallback:BlockFreeButton, onErrorCallback:Error);
        }
        
        private ItemInfo GetRandomItem()
        {
            var number = Random.Range(0, _items.Count-1);
            return _items[number];
        }

        private void FillCell()
        {
            var item = GetRandomItem();
            _levelBack.SetActive(true);
            _itemImag.sprite = item.Icon;
            _levelText.text = item.Level.ToString();
            _playerProgress.AddItemToCurrentItems(item);
        }

        private void Error(string value)
        {
            return;
        }

        private void BlockFreeButton()
        {
            _button.enabled = false;
            _freePanel.SetActive(false);
        }
    }
}