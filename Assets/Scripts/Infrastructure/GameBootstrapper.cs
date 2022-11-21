using System;
using System.Collections;
using Agava.YandexGames;
using Infrastructure.Logic;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain Curtain;

        private Game _game;

        private void Start()
        {
            _game = new Game(this, Curtain);
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}