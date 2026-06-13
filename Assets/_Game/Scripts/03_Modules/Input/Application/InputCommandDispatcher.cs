using Tung.Modules.Input.Command;
using Tung.SharedPorts.Input;

namespace Tung.Modules.Input.Application
{
    public sealed class InputCommandDispatcher : IInputCommandSource
    {
        private readonly CommandBuffer _commandBuffer;
        public InputCommandDispatcher( CommandBuffer commandBuffer )
        {
            _commandBuffer = commandBuffer;
        }
        public bool TryDequeue( out ICommand command)
        {
            return _commandBuffer.TryDequeue(out command);
        }
    }
}