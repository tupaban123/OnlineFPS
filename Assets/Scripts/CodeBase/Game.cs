using OnlineFPS.AssetsManagment;
using OnlineFPS.SceneLoading;
using UnityEngine;

namespace OnlineFPS.CodeBase
{
    public class Game
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;

            Screen.SetResolution(900, 900, false);
        }   

        public void RegisterServices()
        {
            ISceneLoader sceneLoader = new SceneLoader(_coroutineRunner);
            ServiceLocator.Instance.Register<ISceneLoader>(sceneLoader);

            IAssetProvider assetProvider = new AssetProvider();
            ServiceLocator.Instance.Register<IAssetProvider>(assetProvider);
        }

        public void LoadGameScene()
        {
            ServiceLocator.Instance.Get<ISceneLoader>().LoadGame();
        }

        public void LoadMenuScene()
        {
            ServiceLocator.Instance.Get<ISceneLoader>().LoadMenu();
        }
    }
}
