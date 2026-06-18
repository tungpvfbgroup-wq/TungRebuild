
namespace Tung.SharedPorts.Input
{
    public interface IInputContextService
    {
        bool WasSubmitPressedThisFrame();
        void SwitchContext(InputContext targetContext);
    }
}

