using Agava.YandexGames;
using Data;
using Infrastructure.Factory;
using Infrastructure.Logic;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<int>
    {
        private const string INITIAL_POINT_SPAWNER = "InitialPointHeroesSpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly Curtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, Curtain loadingCurtain,
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
            var playerProgress = CreateMainObjects(out var cameraFollower, out var heroesSpawner, out var shopInterface,
                out var progress);
            var follower = cameraFollower.GetComponent<CameraFollower>();
            var wizardsSpawner = heroesSpawner.GetComponent<WizardsSpawner>();
            wizardsSpawner.SetCameraFollower(follower);
            wizardsSpawner.SetupPlayerProgress(progress);
            var uiInventory = shopInterface.GetComponent<UIInventory>();
            follower.SetShopInterface(uiInventory);
            uiInventory.SetPlayerProgress(progress);
            GameObject arenaDisposer = _gameFactory.CreateArenaDisposer();
            GameObject levelFinishInterface = _gameFactory.CreateLevelFinishInterface();
            SubscribePayloads(arenaDisposer, heroesSpawner, uiInventory, progress, levelFinishInterface);
            _stateMachine.Enter<GameLoopState, GameObject, GameObject>(levelFinishInterface, playerProgress);
        }

        private GameObject CreateMainObjects(out GameObject cameraFollower, out GameObject heroesSpawner,
            out GameObject shopInterface, out PlayerProgress progress)
        {
            GameObject playerProgress = _gameFactory.CreatePlayerProgress();
            cameraFollower = _gameFactory.CreateCameraFollower();
            heroesSpawner = _gameFactory.CreateWizardsSpawner(GameObject.FindWithTag(INITIAL_POINT_SPAWNER));
            shopInterface = _gameFactory.CreateShopInterface();
            progress = playerProgress.GetComponent<PlayerProgress>();
            // progress.SaveCurrentSceneNumber();
            return playerProgress;
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