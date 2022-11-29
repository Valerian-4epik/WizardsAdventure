using Agava.YandexGames;
using Data;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace RewardSystem
{
    public class RewardButtonBuyCoinsInDefeatPanel : MonoBehaviour
    {
        private const int REWARD_MONEY = 300;

        [SerializeField] private Button _button;
        [SerializeField] private LevelFinishInterface _levelFinishInterface;

        private PlayerProgress _playerProgress;

        private void OnEnable()
        {
            _playerProgress = _levelFinishInterface.PlayerProgress;
            _button.onClick.AddListener(delegate { Show(); });
        }

        public void Show()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Reward();
            RestartLevel();
#endif
            VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:RestartLevel, onErrorCallback:ErrorReturn);
        }

        private void Reward() => _playerProgress.SaveAllMoney(_playerProgress.LoadAllMoney() + REWARD_MONEY);

        private void ErrorReturn(string value)
        {
            return;
        }

        private void RestartLevel() => _levelFinishInterface.RestartLevel();
    }
}