using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using OnlineFPS.CodeBase;
using OnlineFPS.Constants;

namespace OnlineFPS.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;

        public event Action<float> ProgressChanged;
        public event Action StartLoadScene;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadGame()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(SceneConstants.GameSceneName));
        }

        public void LoadMenu()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(SceneConstants.MenuSceneName));
        }

        public void Restart()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            StartLoadScene?.Invoke();
            AsyncOperation sceneLoadingProgress = SceneManager.LoadSceneAsync(SceneConstants.LoadingSceneName);

            while(!sceneLoadingProgress.isDone)
                yield return null;

            AsyncOperation sceneLoadProgress = SceneManager.LoadSceneAsync(sceneName);

            while(!sceneLoadingProgress.isDone)
            {
                ProgressChanged?.Invoke(sceneLoadProgress.progress / .9f);
                yield return null;
            }
        }
    }
}