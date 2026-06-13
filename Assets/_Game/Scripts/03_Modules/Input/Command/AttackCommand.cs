using Tung.Core.ValueObjects;
using Tung.SharedPorts.Input;

namespace Tung.Modules.Input.Command
{
    public sealed class AttackCommand : IAttackCommand
    {
        public CommandType Type => CommandType.Attack;
        public TungEntityId ControlledEntityId { get; }
        public bool IsHeld { get; }
        public float HeldDuration { get; }
        public AttackCommand(TungEntityId controlledEntityId, bool isHeld, float heldDuration)
        {
            ControlledEntityId = controlledEntityId;
            IsHeld = isHeld;
            HeldDuration = heldDuration;
        }
    }
}