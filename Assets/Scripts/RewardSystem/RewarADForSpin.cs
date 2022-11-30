using Agava.YandexGames;
using Data;
using UI.Roulette;
using UnityEngine;

namespace UI.Services
{
    public class RewarADForSpin : MonoBehaviour
    {
        [SerializeField] private RewardRoulette _roulette;
        [SerializeField] private LevelFinishInterface _levelFinishInterface;
        
        private PlayerProgress _playerProgress;
        private ItemInfo _item;

        public void SetPalayerProgress(PlayerProgress playerProgress) => 
            _playerProgress = playerProgress;

        public void Show()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            PlaySpin();
#endif
            VideoAd.Show(onRewardedCallback:null, onCloseCallback:PlaySpin, onErrorCallback:ErrorReturn);
        }

        private void PlaySpin()
        {
            _roulette.ItemWined += AddItem;
            _roulette.StartCoroutine(_roulette.SpinRoulette());
        }

        private void AddItem()
        {
            _playerProgress.AddItemToCurrentItems(_roulette.ResultItem);
            _levelFinishInterface.NextLevel();
        }

        private void ErrorReturn(string value)
        {
            _levelFinishInterface.NextLevel();
        }
    }
}
