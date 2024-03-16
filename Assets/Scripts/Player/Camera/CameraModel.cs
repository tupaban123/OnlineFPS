using UnityEngine;

namespace OnlineFPS.Player
{
    public class CameraModel
    {
        private const float _multiplier = 0.1f;

        private float _mouseX;
        private float _mouseY;

        public float _xRotation;
        public float _yRotation;

        private CameraView _cameraView;

        public float XRotation => _xRotation;
        public float YRotation => _yRotation;

        public CameraModel(CameraView cameraView)
        {
            _cameraView = cameraView;
        }

        public void OnMouseInput(float x, float y, float sensX, float sensY)
        {
            _mouseX = x;
            _mouseY = y;

            _yRotation += _mouseX * sensX * _multiplier;
            _xRotation -= _mouseY * sensY * _multiplier;

            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        }
    }
}