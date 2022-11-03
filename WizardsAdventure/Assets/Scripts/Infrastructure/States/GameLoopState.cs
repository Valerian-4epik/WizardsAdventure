using Data;
using Infrastructure.Logic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState2<GameObject, GameObject>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private GameObject _levelFinishInterface;
        private PlayerProgress _playerProgress;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(GameObject levelFinishInterface, GameObject playerProgress)
        {
            _levelFinishInterface = levelFinishInterface;
            _playerProgress = playerProgress.GetComponent<PlayerProgress>();
            SubscribeToCompletion();
        }

        public void Exit()
        {
        
        }

        private void SubscribeToCompletion() => 
            _levelFinishInterface.GetComponent<LevelFinishInterface>().LevelEnded += LoadNextLevel;

        private void LoadNextLevel()
        {
            _playerProgress.SaveCurrentSceneNumber();
            _gameStateMachine.Enter<LoadLevelState, int>(_playerProgress.GetNextScene());
        }
    }
}