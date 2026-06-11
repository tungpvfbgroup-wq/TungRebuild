namespace Tung.SharedPorts.Input
{
    public interface IInputCommandSource 
    {
        bool TryQueue(out ICommand command);
    }
}