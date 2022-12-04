using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader //у нас есть инитиал сцена она нужна чтобы предоставить время на загрузку игры без проблем
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) // нам нужно получить внешней зависимостью корутин раннер
            => _coroutineRunner = coroutineRunner;

        public void Load(int payload, Action onLoaded = null)
        {
                _coroutineRunner.StartCoroutine(LoadScene(2, onLoaded));
        }

        // public void Load(int buildIndexNumber) => _coroutineRunner.StartCoroutine(LoadScene(buildIndexNumber));

        //но чтобы перейти на новую сцену нам нужен sceneLoader
        private IEnumerator LoadScene(int nextSceneName, Action onLoaded = null) //чтобы вызвать корутину нужен бехивер
        {
            // if (SceneManager.GetActiveScene().buildIndex == nextSceneName) // проверка 
            // {
            //     onLoaded?.Invoke();//если да то мы вызовем каллбак чтобы удостовериться что все впорядке
            //     yield break; //и прерываем выполнение корутины брейк потомучто ниже уже есть ретерн
            // }
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName); //мы запросили загрузку сцены
            //waitNextScene.completed += _ => onLoaded?.Invoke(); // это называется задискардить _ тоже самое что и default,
            while (!waitNextScene.isDone) //второй способ проверять что операция завершилась isDone
                yield return null; //это своего рода задержка

            onLoaded?.Invoke();
        } // Action это callback а = null это значит что не всегда нам нужно сообщать
    }
}