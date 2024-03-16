using OnlineFPS.CodeBase;
using OnlineFPS.InputSystem;
using UnityEngine;

namespace OnlineFPS.Player
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private float sensX;
        [SerializeField] private float sensY;
        
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform cameraPosition;
        [SerializeField] private Transform character;
        [SerializeField] private Transform neckBone;
        [SerializeField] private Transform headBone;

        private Transform _cameraHolder;

        private CameraModel _model;
        private CameraPresenter _presenter;

        private IInputSystem _inputSystem;

        private bool _isInitialized = false;

        public void Initialize(CameraHolder cameraHolder)
        {
            cameraHolder.Initialize(cameraPosition);
            this._cameraHolder = cameraHolder.transform;

            _model = new CameraModel(this);
            _presenter = new CameraPresenter(_model);

            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

            _inputSystem = ServiceLocator.Instance.Get<IInputSystem>();
            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;

            _presenter.OnMouseInput(_inputSystem.MouseX, _inputSystem.MouseY, sensX, sensY);
            Rotate();
        }

        private void Rotate()
        {
            float xRot = _model.XRotation;
            float yRot = _model.YRotation;

            _cameraHolder.rotation = Quaternion.Euler(xRot, yRot, 0);
            orientation.rotation = Quaternion.Euler(0, yRot, 0);
            character.rotation = Quaternion.Euler(0, yRot, 0);

            float headXRot = 0;
            float neckXRot = -xRot;

            if(xRot > 45)
            {
                headXRot = Mathf.Clamp(-(xRot - 45), -15, 0);
                neckXRot = -45;
            }
            else if(xRot < -45)
            {
                headXRot = Mathf.Clamp((Mathf.Abs(xRot) - 45), 0, 15);
                neckXRot = 45;
            }

            Vector3 newHeadBoneRot = new Vector3(headXRot, neckBone.localEulerAngles.y, neckBone.localEulerAngles.z);
            Vector3 newNeckBoneRot = new Vector3(neckXRot, neckBone.localEulerAngles.y, neckBone.localEulerAngles.z);

            headBone.localEulerAngles = newHeadBoneRot;
            neckBone.localEulerAngles = newNeckBoneRot;
        }

        public Transform GetCameraTransform()
        {
            return _cameraHolder;
        }
    }
}