using OnlineFPS.SceneLoading;
using OnlineFPS.CodeBase;

using UnityEngine;
using UnityEngine.UI;

namespace OnlineFPS.LoadingUI
{
    public class LoadingProgressUI : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;

        private ISceneLoader _sceneLoader;

        private void Start()
        {
            _sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();
            _sceneLoader.ProgressChanged += OnProgressChanged;
        }

        private void OnDestroy()
        {
            if (_sceneLoader != null)
                _sceneLoader.ProgressChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _progressBar.value = progress;
        }
    }
}