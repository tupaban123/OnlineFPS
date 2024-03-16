using OnlineFPS.CodeBase;

namespace OnlineFPS.InputSystem
{
    public interface IInputSystem : IService
    {
        float Horizontal { get; }
        float Vertical { get; }
        bool Jump { get; }
        float MouseX { get; }
        float MouseY { get; }
        public bool IsFireButtonClicked { get; }
        public bool IsFireButtonHolded { get; }
        public bool IsFireButtonReleased { get; }
        public bool IsReloadButtonClicked { get; }
    }
}