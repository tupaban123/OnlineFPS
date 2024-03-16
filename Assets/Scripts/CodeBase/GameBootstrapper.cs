using OnlineFPS.InputSystem;
using OnlineFPS.Network;
using OnlineFPS.Player;
using OnlineFPS.SceneLoading;
using UnityEngine;
using System.Collections.Generic;
using OnlineFPS.UI.PlayerStats;

namespace OnlineFPS.CodeBase
{
    public class GameBootstrapper : MonoBehaviour, IService
    {
        [SerializeField] private CameraHolder _cameraHolder;
        [SerializeField] private PlayerStatsUI _uiPlayerStats;

        private List<PlayerView> _players = new List<PlayerView>();

        private bool _playersInitialized = false;

        private void Start()
        {
            IInputSystem inputSystem = new StandaloneInputSystem();
            ServiceLocator.Instance.Register<IInputSystem>(inputSystem);

            ServiceLocator.Instance.Register<GameBootstrapper>(this);

            if (_uiPlayerStats != null)
                ServiceLocator.Instance.Register<PlayerStatsUI>(_uiPlayerStats);

            ServiceLocator.Instance.Get<ISceneLoader>().StartLoadScene += OnStartLoadOtherScene;
            CustomNetworkManager.OnClientConnected += InitializePlayers;

            InitializePlayers();
        }

        private void OnDestroy()
        {
            CustomNetworkManager.OnClientConnected -= InitializePlayers;
            ServiceLocator.Instance.Get<ISceneLoader>().StartLoadScene -= OnStartLoadOtherScene;
        }

        private void OnStartLoadOtherScene()
        {
            ServiceLocator.Instance.Unregister<IInputSystem>();
            ServiceLocator.Instance.Unregister<GameBootstrapper>();
            ServiceLocator.Instance.Unregister<PlayerStatsUI>();
        }

        private void InitializePlayers()
        {
            foreach(var player in _players)
            {
                if (!player.IsInitialized)
                    player.Initialize(_cameraHolder);
            }

            _playersInitialized = true;
        }

        public void AddPlayer(PlayerView player)
        {
            _players.Add(player);

            if (_playersInitialized)
                player.Initialize(_cameraHolder);
        }
    }
}