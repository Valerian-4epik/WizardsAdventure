using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishInterface : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private ArenaDisposer _arenaDisposer;

    public event Action LevelEnded;
    public event Action LevelDefeat;

    public void NextLevel()
    {
        InterstitialAd.Show(null, GoNextLevel, null);
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

    private void ActivatePanel(bool isWin)
    {
        if (isWin)
            _winPanel.SetActive(true);
        else
            _losePanel.SetActive(true);
    }
}