using System;
using Agava.YandexGames;
using Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Services
{
    public class RewardADForBuyCoins : MonoBehaviour
    {
        private const int REWARD_MONEY = 300;

        [SerializeField] private Button _button;
        [SerializeField] private UIInventory _inventory;
        [SerializeField] private GameObject _panel;

        private PlayerProgress _progressData;

        private void OnEnable()
        {
            _progressData = _inventory.PlayerProgress;
            _button.onClick.AddListener(delegate { Show(); });
        }

        public void Show()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Reward();
            PlaySoundFx();
            CloseButton();
#endif
            VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:CloseButton, onErrorCallback:ErrorReturn);
        }

        private void Reward() => _progressData.SaveCurrentMoney(_progressData.LoadCurrentMoney() + REWARD_MONEY);

        private void ErrorReturn(string value)
        {
            return;
        }

        private void CloseButton()
        {
            PlaySoundFx();
            _panel.SetActive(false);
        }

        private void PlaySoundFx() => _inventory.PlayGoldBuy();
    }
}