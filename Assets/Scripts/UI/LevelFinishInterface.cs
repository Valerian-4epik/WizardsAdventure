using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Data;
using RewardSystem;
using UI.Services;
using UnityEngine;
using UnityEngine.Audio;

public class LevelFinishInterface : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private RewarADForSpin _rewarAD;
    [SerializeField] private GameObject _oneLevelBack;

    private ArenaDisposer _arenaDisposer;
    private PlayerProgress _playerProgress;
    private List<ItemInfo> _rewardItems = new List<ItemInfo>();

    public List<ItemInfo> RewardItems => _rewardItems;
    public PlayerProgress PlayerProgress => _playerProgress;

    public event Action LevelEnded;
    public event Action<bool> LevelDefeat;

    public void NextLevel()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnSwitchMusicVolume(false);
        GoNextLevel(true);
#endif
        OnSwitchMusicVolume(false);
        InterstitialAd.Show(null, GoNextLevel, GoNextLevel);
    }

    public void RestartLevel() =>
        LevelDefeat?.Invoke(true);
    
    public void OneLevelBack() =>
        LevelDefeat?.Invoke(false);
    
    public void SubscribeToEndFight(ArenaDisposer arenaDisposer)
    {
        _arenaDisposer = arenaDisposer;
        GetPlayerProgress();
    }

    public void GoNextLevel(string value) => LevelEnded?.Invoke();
    
    public void ActivatePanel(bool isWin)
    {
        if (isWin)
        {
            _winPanel.SetActive(true);
            _winPanel.GetComponent<RewardItemGenerator>().GetPlayerProgress(_arenaDisposer.PlayerProgress);
            _rewarAD.SetPalayerProgress(_arenaDisposer.PlayerProgress);
        }
        else
        {
            _losePanel.SetActive(true);

            if (_playerProgress.GetLevel() == 1)
                _oneLevelBack.SetActive(false);
        }
    }

    private void GetPlayerProgress() => _playerProgress = _arenaDisposer.PlayerProgress;

    private void GoNextLevel(bool value)
    {
        SaveRewardItems();
        LevelEnded?.Invoke();
    }

    private void SaveRewardItems(Action onEndedCallback = null)
    {
        foreach (var item in _rewardItems)
        {
            _playerProgress.AddItemToCurrentItems(item);
        }

        AddRewardMoney();
        onEndedCallback?.Invoke();
    }
    
    private void AddRewardMoney() => _playerProgress.AddReward();
    
    private void OnSwitchMusicVolume(bool value)
    {
        if (value)
            _audioMixer.audioMixer.SetFloat("Master", 0);
        else
            _audioMixer.audioMixer.SetFloat("Master", -80);
    }
}