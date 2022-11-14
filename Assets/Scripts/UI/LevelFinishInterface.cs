using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishInterface : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private ArenaDisposer _arenaDisposer;
    
    public event Action LevelEnded;
    public event Action LevelDefeat; 

    public void NextLevel() =>  
        LevelEnded?.Invoke();

    public void RestartLevel() =>
        LevelDefeat?.Invoke(); 

    public void SubscribeToEndFight(ArenaDisposer arenaDisposer)
    {
        _arenaDisposer = arenaDisposer;
        _arenaDisposer.EndFight += ActivatePanel;
    }

    private void ActivatePanel(bool isWin)
    {
        if(isWin)
            _winPanel.SetActive(true);
        else
            _losePanel.SetActive(true);
    }
}
