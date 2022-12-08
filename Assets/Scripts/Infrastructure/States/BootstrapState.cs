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
        private const int MENU_SCENE = 1;

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private IGameFactory _gameFactory;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
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
            // _gameStateMachine.Enter<LoadLevelState, int>(1);
            if (IsTutorialStart())
                _gameStateMachine.Enter<LoadLevelState, int>(2);
            else
                _gameStateMachine.Enter<LoadLevelState, int>(1);
        }

        private bool IsTutorialStart()
        {
            _gameFactory = _services.Single<IGameFactory>();
            GameObject playerProgress = _gameFactory.CreatePlayerProgress();
            var progress = playerProgress.GetComponent<PlayerProgress>();
            return progress.GetStartTutorialInfo();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
            _services.RegisterSingle<IYandexListener>( new YandexListener());
        }
    }

    public interface IYandexListener : IService
    {
    }

    public class YandexListener : IYandexListener
    {
        
    }
}