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
        private const int MENU_SCENE = 1;
        private const int LEVEL_FIRST = 2;
        private const int CONTINUE_KODE = 99;


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
            // var progress = _gameFactory.CreatePlayerProgress().GetComponent<PlayerProgress>();
            _loadingCurtain.Show();

            // if (payload == MENU_SCENE)
            // {
            //     _sceneLoader.Load(payload);
            //     _loadingCurtain.Hide();
            // }
            // else if (payload == LEVEL_FIRST)
            // {
            //     progress.UpdateStatistics();
            //     progress.SaveGameStata(false);
            //     _sceneLoader.Load(payload, OnLoaded);
            // }
            // else if (payload == CONTINUE_KODE)
            // {
            //     if (!progress.GetGameState())
            //         _sceneLoader.Load(progress.GetNextScene(), OnLoaded);
            // }
            
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            var playerProgress = CreateMainObjects(out var cameraFollower, out var heroesSpawner, out var shopInterface,
                out var progress);
            heroesSpawner.GetComponent<WizardsSpawner>().SetupPlayerProgress(progress);
            var uiInventory = shopInterface.GetComponent<UIInventory>();
            uiInventory.SetPlayerProgress(progress);
            GameObject arenaDisposer = _gameFactory.CreateArenaDisposer();
            GameObject levelFinishInterface = _gameFactory.CreateLevelFinishInterface();
            SubscribePayloads(arenaDisposer, heroesSpawner, uiInventory, progress, levelFinishInterface);
            cameraFollower.GetComponent<CameraFollower>().SetShopInterface(uiInventory);

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