using System;
using Agava.YandexGames;
using UI.Services;
using UnityEngine;

public class LevelFinishInterface : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private RewarADForSpin _rewarAD;

    private ArenaDisposer _arenaDisposer;

    public event Action LevelEnded;
    public event Action LevelDefeat;

    public void NextLevel()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        GoNextLevel(true);
#endif
        InterstitialAd.Show(null, GoNextLevel, GoNextLevel);
    }

    public void RestartLevel() =>
        LevelDefeat?.Invoke();

    public void SubscribeToEndFight(ArenaDisposer arenaDisposer)
    {
        _arenaDisposer = arenaDisposer;
        _arenaDisposer.EndFight += ActivatePanel;
    }

    private void GoNextLevel(bool value)
    {
        if (value == true)
        {
            LevelEnded?.Invoke();
        }
    }

    private void GoNextLevel(string value) => LevelEnded?.Invoke();

    private void ActivatePanel(bool isWin)
    {
        if (isWin)
        {
            _winPanel.SetActive(true); 
            _rewarAD.SetPalayerProgress(_arenaDisposer.PlayerProgress);
        }
        else
            _losePanel.SetActive(true);
    }
}