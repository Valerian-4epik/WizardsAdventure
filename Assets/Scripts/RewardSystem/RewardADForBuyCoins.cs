using Agava.YandexGames;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Services
{
    public class RewardADForBuyCoins : MonoBehaviour
    {
        private const int REWARD_MONEY = 200;

        [SerializeField] private Button _button;
        [SerializeField] private UIInventory _inventory;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _text;

        private PlayerProgress _progressData;
        private int _currentLevel;

        private void OnEnable()
        {
            _progressData = _inventory.PlayerProgress;
            _currentLevel = _progressData.GetLevel();
            _text.text = GetRewardValue().ToString();
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

        private void Reward() => _progressData.SaveCurrentMoney(_progressData.LoadCurrentMoney() + GetRewardValue());

        private void ErrorReturn(string value)
        {
            _panel.SetActive(false);
        }

        private void CloseButton()
        {
            PlaySoundFx();
            _panel.SetActive(false);
        }

        private void PlaySoundFx() => _inventory.PlayGoldBuy();
        
        private int GetRewardValue()
        {
            if (_currentLevel < 12)
                return 200;
            if (_currentLevel < 24)
                return 300;
            if (_currentLevel < 36)
                return 500;
            if (_currentLevel < 48)
                return 1000;
            if (_currentLevel < 60)
                return 2000;
            if (_currentLevel < 72)
                return 3000;

            return 200;
        }
    }
}
