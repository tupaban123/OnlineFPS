namespace OnlineFPS.Player
{
    public class CameraPresenter
    {
        private CameraModel _cameraModel;

        public CameraPresenter(CameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        public void OnMouseInput(float mouseX, float mouseY, float sensX, float sensY)
        {
            _cameraModel.OnMouseInput(mouseX, mouseY, sensX, sensY);
        }
    }
}