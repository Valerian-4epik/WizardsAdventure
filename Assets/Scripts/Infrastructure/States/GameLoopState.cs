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
        private readonly Curtain _loadingCurtain;
        private GameObject _levelFinishInterface;
        private PlayerProgress _playerProgress;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, Curtain loadingCurtain)
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

        private void SubscribeToCompletion()
        {
            var levelFinishInterface = _levelFinishInterface.GetComponent<LevelFinishInterface>();
            levelFinishInterface.LevelEnded += LoadNextLevel;
            levelFinishInterface.LevelDefeat += LoadCurrentLevel;
        }


        private void LoadNextLevel()
        {
            _playerProgress.SaveLevel(_playerProgress.GetLevel()+1);
            _gameStateMachine.Enter<LoadLevelState, int>(1);
        }

        private void LoadCurrentLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, int>(1);
        }
    }
}