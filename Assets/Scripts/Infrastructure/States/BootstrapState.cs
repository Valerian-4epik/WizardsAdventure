using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState //начальный стейт с которого все начинается.
    {
        private const int INITIAL_SCENE = 0;

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private IGameFactory _gameFactory;

        //бутстрапу нужна сылка на стейт машну чтобы после завершение своего функцианала сообщить машине поехали дальше
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() //задача бутстрапа привести нас в началло и регистрировать сервисы 
        {
            _sceneLoader.Load(INITIAL_SCENE, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
             var nextSceneNumber = GetScene();
            _gameStateMachine.Enter<LoadLevelState, int>(nextSceneNumber);
        }

        private int GetScene()
        {
            _gameFactory = _services.Single<IGameFactory>();
            GameObject playerProgress = _gameFactory.CreatePlayerProgress();
            var nextScene = playerProgress.GetComponent<PlayerProgress>().GetNextScene();
            return nextScene;
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
        }
    }
}