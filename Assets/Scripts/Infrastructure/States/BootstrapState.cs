using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const int INITIAL_SCENE = 0;

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private IGameFactory _gameFactory;
        private PlayerProgress _playerProgress;
        private bool _isTutorialStart;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
            _playerProgress = CreateProgress();
            _isTutorialStart = _playerProgress.GetStartTutorialInfo();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL_SCENE, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, int>(_isTutorialStart ? 2 : 1);
        }
        
        private PlayerProgress CreateProgress()
        {
            _gameFactory = _services.Single<IGameFactory>();
            GameObject playerProgress = _gameFactory.CreatePlayerProgress();
            var progress = playerProgress.GetComponent<PlayerProgress>();
            return progress;
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }
    }
}