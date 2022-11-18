using System;
using Infrastructure.Logic;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private const int LEVEL_FIRST = 2;
        private const int CONTINUE_KODE = 99;
        public LoadingCurtain Curtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Curtain);
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
        
        public void PlayNewGame() => 
            _game.GameStateMachine.Enter<LoadLevelState, int>(LEVEL_FIRST);

        public void ContinueGame() =>
            _game.GameStateMachine.Enter<LoadLevelState, int>(CONTINUE_KODE);
    }
}