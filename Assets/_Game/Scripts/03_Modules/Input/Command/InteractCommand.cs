using Tung.Core.ValueObjects;
using Tung.SharedPorts.Input;
namespace Tung.Modules.Input.Command
{
    public sealed class InteractCommand : IInteractCommand
    {
        public CommandType Type => CommandType.Interact;
        public TungEntityId ControlledEntityId { get; }
        public InteractCommand(TungEntityId controlledEntityId)
        {
            ControlledEntityId = controlledEntityId;
        }
    }
}