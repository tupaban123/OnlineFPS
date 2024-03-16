using OnlineFPS.Configs;
using OnlineFPS.Player.Health;
using UnityEngine;

namespace OnlineFPS.Player
{
    public class PlayerModel
    {
        private const float _movementMultiplier = 4f;

        private Transform _orientation;

        private PlayerView _playerView;
        private PlayerConfig _playerConfig;
        private PlayerHealthView _playerHealth;

        private float _horizontalInput;
        private float _verticalInput;

        private bool _onSlope;

        private RaycastHit _slopeHit;

        private Transform Transform => _playerView.transform;

        public Vector3 MovementDirection => _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        public Vector3 SlopeMovementDirection => Vector3.ProjectOnPlane(MovementDirection, _slopeHit.normal);

        public Vector3 MoveForce 
        { 
            get
            {
                var currentMovementDirection = _onSlope ? SlopeMovementDirection : MovementDirection;

                return currentMovementDirection * _playerConfig.Speed * _movementMultiplier;
            }
        }

        public PlayerModel(PlayerView playerView, PlayerConfig playerConfig, PlayerHealthView playerHealth, Transform orientation)
        {
            _playerView = playerView;
            _playerConfig = playerConfig;
            _playerHealth = playerHealth;   
            _orientation = orientation;
        }

        public void OnMoveInput(float horizontal, float vertical)
        {
            _horizontalInput = horizontal;
            _verticalInput = vertical;

            _playerView.ChangeAnimatorInput((int)vertical);
        }

        public void OnJumpInput()
        {
            _playerView.Jump(Transform.up * _playerConfig.JumpForce);
        }

        public void OnSlopeRaycast(RaycastHit slopeHit)
        {
            _slopeHit = slopeHit;

            _onSlope = slopeHit.normal != Vector3.up;
        }
        
        public void NoSlopeRaycast()
        {
            _onSlope = false;
        }
    }
}