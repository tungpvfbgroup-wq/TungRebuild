using Tung.Core.ValueObjects;

namespace Tung.SharedPorts.Input
{
    public interface ICommand
    {
        CommandType Type { get; }
        TungEntityId ControlledEntityId { get; }
    }
}