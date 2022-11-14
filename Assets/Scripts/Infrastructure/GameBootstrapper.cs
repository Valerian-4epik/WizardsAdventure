using System;
using Infrastructure.Logic;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner // геймбутстраппер создает игру
    {
        public LoadingCurtain Curtain;
        //он содержиться на сцене единственным компанентом
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Curtain); //мы содали игру, а там есть стейт машина
            //игре нужен ICorotineRunner а бустраппе и является корутин раннером поэто this
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}