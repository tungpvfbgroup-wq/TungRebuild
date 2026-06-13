using Tung.Core.ValueObjects;
using Tung.SharedPorts.Input;

namespace Tung.Modules.Input.Command
{
    public sealed class SwitchContextCommand : ICommand
    {
        public CommandType Type => CommandType.SwitchContext;
        public TungEntityId ControlledEntityId { get; }
        public InputContext TargetContext;
        public SwitchContextCommand(TungEntityId controlledEntityId, InputContext targetContext)
        {
            ControlledEntityId = controlledEntityId;
            TargetContext = targetContext;
        }
    }
}