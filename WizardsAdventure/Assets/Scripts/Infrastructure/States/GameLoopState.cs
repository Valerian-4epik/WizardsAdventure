using Infrastructure.Logic;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameObject>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private GameObject _levelFinishInterface;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(GameObject levelFinishInterface)
        {
            _levelFinishInterface = levelFinishInterface;
            SubscribeToCompletion();
        }

        public void Exit()
        {
        
        }

        private void SubscribeToCompletion()
        {
            _levelFinishInterface.GetComponent<LevelFinishInterface>().LevelEnded += LoadNewLevel;
        }

        private void LoadNewLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Level2");
        }
    }
}