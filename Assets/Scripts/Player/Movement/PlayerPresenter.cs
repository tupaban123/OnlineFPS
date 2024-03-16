using System;
using UnityEngine;

namespace OnlineFPS.Player
{
    public class PlayerPresenter
    {
        private PlayerModel _playerModel;

        public PlayerPresenter(PlayerModel playerModel)
        {
            _playerModel = playerModel; 
        }

        public void OnMoveInput(float horizontal, float vertical)
        {
            _playerModel.OnMoveInput(horizontal, vertical);
        }

        public void OnSlopeRaycast(RaycastHit slopeHit)
        {
            _playerModel.OnSlopeRaycast(slopeHit);
        }
        
        public void OnNoSlopeRaycast()
        {
            _playerModel.NoSlopeRaycast();
        }

        public void OnJumpInput()
        {
            _playerModel.OnJumpInput();
        }
    }
}