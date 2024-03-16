using System;

using OnlineFPS.CodeBase;

namespace OnlineFPS.SceneLoading
{
    public interface ISceneLoader : IService
    {
        event Action<float> ProgressChanged;
        event Action StartLoadScene;

        void LoadMenu();
        void LoadGame();
        void Restart();
    }
}