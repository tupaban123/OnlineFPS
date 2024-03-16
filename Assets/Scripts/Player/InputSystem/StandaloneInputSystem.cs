using UnityEngine;

namespace OnlineFPS.InputSystem
{
    public class StandaloneInputSystem : IInputSystem
    {
        public float Horizontal => Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;

        public float Vertical => Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;

        public bool Jump => Input.GetKeyDown(KeyCode.Space);

        public float MouseX => Input.GetAxisRaw("Mouse X");

        public float MouseY => Input.GetAxisRaw("Mouse Y");

        public bool IsFireButtonReleased => Input.GetMouseButtonUp(0);

        public bool IsFireButtonClicked => Input.GetMouseButtonDown(0);

        public bool IsFireButtonHolded => Input.GetMouseButton(0);

        public bool IsReloadButtonClicked => Input.GetKeyDown(KeyCode.R);
    }

}