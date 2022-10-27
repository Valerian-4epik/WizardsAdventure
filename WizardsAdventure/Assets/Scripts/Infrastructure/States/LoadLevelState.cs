using Infrastructure.Factory;
using Infrastructure.Logic;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string> //так как интерфейс пайлоадед принимает в себя условие
    {
        private const string Initialpointspawner = "InitialPointHeroesSpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }
    
        public void Enter(string nameScene)
        {
            _loadingCurtain.Show(); //перед загрузкой покажем.
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()//тут я зягружаю все обьекты и поведения которые должны быть на сцене 
        {
            GameObject heroesSpawner = _gameFactory.CreateWizardsSpawner(GameObject.FindWithTag(Initialpointspawner));// at где создать
            GameObject shopInterface = _gameFactory.CreateShopInterface();
            GameObject arenaDisposer = _gameFactory.CreateArenaDisposer();
            arenaDisposer.GetComponent<ArenaDisposer>().SetShopInterface(shopInterface.GetComponent<UIInventory>());
        
            //shop
            //spawner
            //cameraFolower
            _stateMachine.Enter<GameLoopState>();
        }
    }
}