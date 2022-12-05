using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader //у нас есть инитиал сцена она нужна чтобы предоставить время на загрузку игры без проблем
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
            => _coroutineRunner = coroutineRunner;

        public void Load(int payload, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(payload, onLoaded));

        public void Load(int buildIndexNumber) => _coroutineRunner.StartCoroutine(LoadScene(buildIndexNumber));
        
        private IEnumerator LoadScene(int nextSceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().buildIndex == nextSceneName && SceneManager.GetActiveScene().buildIndex != 1) 
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);
            // yield return null;
            
            // SceneManager.LoadScene(nextSceneName);
            // waitNextScene.completed += _ => onLoaded?.Invoke();
            
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}