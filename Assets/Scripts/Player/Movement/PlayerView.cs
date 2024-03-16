using UnityEngine;
using OnlineFPS.Configs;
using OnlineFPS.InputSystem;
using OnlineFPS.CodeBase;
using OnlineFPS.Constants;
using Mirror;
using OnlineFPS.GunBase;
using OnlineFPS.AssetsManagment;
using OnlineFPS.Player.Health;

namespace OnlineFPS.Player
{
    public class PlayerView : NetworkBehaviour
    {
        [Header("Objects")]
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform laserSpawnpoint;
        [SerializeField] private GameObject characterObject;

        [Header("Components")]
        [SerializeField] private Animator animator;
        [SerializeField] private CameraView cameraView;
        [SerializeField] private PlayerHealthView _playerHealthView;

        [Header("Guns")]
        [SerializeField] private GunView[] guns;

        private PlayerModel _playerModel;
        private PlayerPresenter _playerPresenter;

        private PlayerConfig _playerConfig;

        private Rigidbody _rigidbody;

        private IInputSystem _inputSystem;

        private bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        private void Start()
        {
            ServiceLocator.Instance.Get<GameBootstrapper>().AddPlayer(this);
        }

        public void Initialize(CameraHolder cameraHolder)
        {
            _playerConfig = ServiceLocator.Instance.Get<IAssetProvider>().GetPlayerConfig();

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            
            foreach (var gun in guns)
                gun.Initialize(this);

            _playerHealthView.Initialize(_playerConfig.MaxHealth, this);

            if (!isLocalPlayer)
            {
                Debug.Log($"{gameObject.name} not local player");
                Destroy(cameraView);
                return;
            }
            else
                Debug.Log($"{gameObject.name} localplayer");

            if (_isInitialized)
            {
                Debug.Log("already initialized");
                return;
            }
            
            _playerModel = new PlayerModel(this, _playerConfig, _playerHealthView, orientation);
            _playerPresenter = new PlayerPresenter(_playerModel);
        
            _inputSystem = ServiceLocator.Instance.Get<IInputSystem>();

            cameraView.Initialize(cameraHolder);
            _isInitialized = true;

            Debug.Log($"{gameObject.name} initialized");
        }

        private void Update()
        {
            if (!isLocalPlayer || !_isInitialized)
                return;

            if(Physics.Raycast(laserSpawnpoint.position, laserSpawnpoint.forward, out RaycastHit hit))
            {
                Debug.DrawLine(laserSpawnpoint.position, hit.point, Color.red);
            }

            if (_inputSystem != null)
            {
                _playerPresenter.OnMoveInput(_inputSystem.Horizontal, _inputSystem.Vertical);

                if (_inputSystem.Jump)
                    _playerPresenter.OnJumpInput();
            }

            CheckSlope();
        }

        private void FixedUpdate()
        {
            if (!isLocalPlayer || !_isInitialized)
                return;

            Move(_playerModel.MoveForce);
        }

        private void Move(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void Jump(Vector3 force)
        {
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        public void ChangeAnimatorInput(int value)
        {
            animator.SetInteger(PlayerAnimatorParameters.VerticalInput, value);
        }

        public void EnableCharacter(bool enabled) 
        { 
            characterObject.SetActive(enabled);
            _rigidbody.isKinematic = !enabled;
        }

        public void CheckSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, _playerConfig.SlopeRayLength))
                _playerPresenter.OnSlopeRaycast(slopeHit);
            else
                _playerPresenter.OnNoSlopeRaycast();
        }

        public Transform GetCameraTransform()
        {
            return cameraView.GetCameraTransform();
        }
        
        public Transform GetLaserSpawnpointTransform()
        {
            Debug.Log($"Getting laser spawnpoint. {laserSpawnpoint != null}");
            return laserSpawnpoint;
        }
    }
}