using UnityEngine;
using OnlineFPS.Configs;
using OnlineFPS.InputSystem;
using OnlineFPS.CodeBase;
using OnlineFPS.AssetsManagment;
using OnlineFPS.Player;
using Mirror;
using OnlineFPS.Player.Health;

namespace OnlineFPS.GunBase
{
    public abstract class GunView : NetworkBehaviour, ICoroutineRunner
    {
        [SerializeField] private string gun_id;

        private GunConfig _gunConfig;

        protected GunModel _gunModel;   
        private GunPresenter _gunPresenter;

        private PlayerView _player;

        protected IInputSystem _inputSystem;

        protected bool _isInitialized = false;

        protected Transform _laserSpawnpoint;

        [SyncVar]
        protected bool _isFireing;

        public void Initialize(PlayerView playerView)
        {
            _player = playerView;

            _gunConfig = ServiceLocator.Instance.Get<IAssetProvider>().GetGunConfigByID(gun_id);

            if (_gunConfig == null)
            {
                Debug.Log($"Gun config not founded on {name}");
                return;
            }

            _laserSpawnpoint = _player.GetLaserSpawnpointTransform();

            if (!isLocalPlayer)
                return;

            _gunModel = new GunModel(this, _gunConfig, this);
            _gunPresenter = new GunPresenter(_gunModel, this);

            _inputSystem = ServiceLocator.Instance.Get<IInputSystem>();
            _isInitialized = true;
        }

        protected virtual void Update()
        {
            if (!_isInitialized)
                return;

            if (ReadFireInput())
                _gunPresenter.OnFire();

            if (_inputSystem.IsReloadButtonClicked)
                _gunPresenter.StartReloading();
            
            ToggleFireing(_gunModel.IsReloading ? false : ReadFireInput());
        }

        private bool ReadFireInput()
        {
            switch(_gunConfig.GunType)
            {
                case GunType.Automatic:
                    return _inputSystem.IsFireButtonHolded;
                case GunType.SingleAction:
                    return _inputSystem.IsFireButtonClicked;
            }

            return false;
        }

        [Command]
        public virtual void CmdFire()
        {
            var laserPos = _laserSpawnpoint.position;
            var laserLookDirection = _laserSpawnpoint.forward;

            if (Physics.Raycast(laserPos, laserLookDirection, out RaycastHit hit))
            {
                Debug.Log($"{_player.name} hitted {hit.collider.name} with {name}");

                if (hit.collider.TryGetComponent(out IDamagable damagableHit))
                {
                    damagableHit.Damage(_gunConfig.Damage);
                }
            }
        }

        [Command]
        public void ToggleFireing(bool value) => _isFireing = value;
    }
}