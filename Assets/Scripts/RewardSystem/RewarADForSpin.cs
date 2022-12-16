using Agava.YandexGames;
using Data;
using UI.Roulette;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Services
{
    public class RewarADForSpin : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private RewardRoulette _roulette;
        [SerializeField] private LevelFinishInterface _levelFinishInterface;
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _nextLevelButton;
        
        private PlayerProgress _playerProgress;
        private ItemInfo _item;

        public void SetPalayerProgress(PlayerProgress playerProgress) => 
            _playerProgress = playerProgress;

        public void Show()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            ButtonsBlock();
            PlaySpin();
#endif
            OnSwitchMusicVolume(false);
            ButtonsBlock();
            VideoAd.Show(onRewardedCallback:Reward, onCloseCallback:PlaySpin, onErrorCallback:ErrorReturn);
        }

        private void PlaySpin()
        {
            _roulette.ItemWined += AddItem;
            StartCoroutine(_roulette.SpinRoulette());
        }

        private void AddItem()
        {
            _playerProgress.AddItemToCurrentItems(_roulette.ResultItem);
            _levelFinishInterface.NextLevel();
        }

        private void ErrorReturn(string value)
        {
            OnSwitchMusicVolume(true);
            _levelFinishInterface.NextLevel();
        }

        private void Reward()
        {
            OnSwitchMusicVolume(false);
        }

        private void ButtonsBlock()
        {
            _spinButton.interactable = false;
            _nextLevelButton.interactable = false;
        }

        private void OnSwitchMusicVolume(bool value)
        {
            if (value)
                _audioMixer.audioMixer.SetFloat("Master", 0);
            else
                _audioMixer.audioMixer.SetFloat("Master", -80);
        }
    }
}
