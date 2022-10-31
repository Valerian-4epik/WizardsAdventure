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
            _loadingCurtain.Show();
            _sceneLoader.Load(nameScene, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            GameObject cameraFollower = _gameFactory.CreateCameraFollower();
            GameObject heroesSpawner = _gameFactory.CreateWizardsSpawner(GameObject.FindWithTag(Initialpointspawner));// at где создать
            GameObject shopInterface = _gameFactory.CreateShopInterface();
            var uiInventory = shopInterface.GetComponent<UIInventory>();
            GameObject arenaDisposer = _gameFactory.CreateArenaDisposer();
            var disposer = arenaDisposer.GetComponent<ArenaDisposer>();
            disposer.SetWizardSpawner(heroesSpawner);
            disposer.SetShopInterface(uiInventory);
            cameraFollower.GetComponent<CameraFollower>().SetShopInterface(uiInventory);
            GameObject levelFinishInterface = _gameFactory.CreateLevelFinishInterface();
            disposer.SetLevelFinishInterface(levelFinishInterface);
            
            _stateMachine.Enter<GameLoopState, GameObject>(levelFinishInterface);
        }
    }
}