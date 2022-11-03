using Data;
using Infrastructure.Factory;
using Infrastructure.Logic;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<int> //так как интерфейс пайлоадед принимает в себя условие
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
        
        public void Enter(int payload)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            GameObject playerProgress = _gameFactory.CreatePlayerProgress();
            GameObject cameraFollower = _gameFactory.CreateCameraFollower();
            GameObject heroesSpawner = _gameFactory.CreateWizardsSpawner(GameObject.FindWithTag(Initialpointspawner));// at где создать
            GameObject shopInterface = _gameFactory.CreateShopInterface();
            var progress = playerProgress.GetComponent<PlayerProgress>();
            heroesSpawner.GetComponent<WizardsSpawner>().SetupPlayerProgress(progress);
            var uiInventory = shopInterface.GetComponent<UIInventory>();
            uiInventory.SetPlayerProgress(progress);
            GameObject arenaDisposer = _gameFactory.CreateArenaDisposer();
            GameObject levelFinishInterface = _gameFactory.CreateLevelFinishInterface();
            SubscribePayloads(arenaDisposer, heroesSpawner, uiInventory, progress, levelFinishInterface);
            cameraFollower.GetComponent<CameraFollower>().SetShopInterface(uiInventory);
            
            _stateMachine.Enter<GameLoopState, GameObject, GameObject>(levelFinishInterface, playerProgress);
        }

        private void SubscribePayloads(GameObject arenaDisposer, GameObject heroesSpawner, UIInventory uiInventory,
            PlayerProgress progress, GameObject levelFinishInterface)
        {
            var disposer = arenaDisposer.GetComponent<ArenaDisposer>();
            disposer.SetWizardSpawner(heroesSpawner);
            disposer.SetShopInterface(uiInventory);
            disposer.SetPlayerProgress(progress);
            disposer.SetLevelFinishInterface(levelFinishInterface);
        }
    }
}