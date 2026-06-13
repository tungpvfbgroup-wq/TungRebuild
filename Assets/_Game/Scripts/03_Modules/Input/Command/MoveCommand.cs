using Tung.Core.ValueObjects;
using Tung.SharedPorts.Input;
namespace Tung.Modules.Input.Command
{
    public sealed class MoveCommand : IMoveCommand
    {
        public CommandType Type => CommandType.Move;
        public TungEntityId ControlledEntityId { get; }
        public float DirX { get; }
        public float DirY { get; }
        public MoveCommand(TungEntityId controlledEntityId, float dirX, float dirY)
        {
            ControlledEntityId = controlledEntityId;
            DirX = dirX;
            DirY = dirY;
        }
        public bool IsMoving => DirX != 0f || DirY != 0f;
    }
}
