using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState //начальный стейт с которого все начинается.
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services; //по факту это конченая статика

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
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() // этот переход в следующий стейт
            => _gameStateMachine.Enter<LoadLevelState, string>("Main");

        private void RegisterServices()
        {
            Debug.Log("Я зарегестрировал сервисы");
            //допустим зарегестрировать импут сервис если он у вас есть

            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssets>()));
            //регистрируем сервис как сингл инстанс сервис
        }
    }
}