using OnlineFPS.CodeBase;
using OnlineFPS.SceneLoading;

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        ServiceLocator.Instance.Get<ISceneLoader>().LoadGame();
    }
}
