using System;
using Tung.Core.ValueObjects;
using Tung.Modules.Input.Command;
using Tung.SharedPorts.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Tung.Modules.Input.Infrastructure
{
    public sealed class InputReader : MonoBehaviour, IInputContextService
    {
        [SerializeField] private InputActionAsset _action;
        private CommandBuffer _commandBuffer;
        private TungEntityId _entityId;
        private InputActionGateway _inputActionGateway;
        public void SetControlledEntityId(TungEntityId entityId)
        {
            if (!entityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires a valid controlled entityId");
            }
            _entityId = entityId;
        }
        [Inject]
        public void Inject(CommandBuffer commandbuffer)
        {
            _commandBuffer = commandbuffer ?? throw new ArgumentNullException(nameof(commandbuffer));
        }
        private InputActionGateway EnsureGateway()
        {
            if (_action == null)
            {
                throw new InvalidOperationException("InputReader requires an active InputActionAsset");
            }
            _inputActionGateway ??= new InputActionGateway(_action);
            return _inputActionGateway;
        }
        public void ValidateConfiguration()
        {
            _ = EnsureGateway();
        }
        public void Awake()
        {
            _inputActionGateway = EnsureGateway();
            _inputActionGateway.SetContext(InputContext.Player);
        }
        private void OnEnable()
        {
            _inputActionGateway.EnableCurrentContext();
        }
        private void OnDisable()
        {
            _inputActionGateway.DisableCurrentContext();
        }
        private void Update()
        {
            switch (_inputActionGateway.CurrentContext)
            {
                case InputContext.Player: ReadPlayerMap(); return;
                case InputContext.UI: return;
                case InputContext.Vehicle: throw new InvalidOperationException($"InputReader does not support Vehicle context yet");
                case null: throw new InvalidOperationException("Input reader requires an active InputContext before update runs");
                default: throw new InvalidOperationException($"InputReader does not support {_inputActionGateway.CurrentContext}");
            }
        }
        private void ReadPlayerMap()
        {
            if (_commandBuffer == null)
            {
                throw new InvalidOperationException("InputReader requires a CommandBuffer before update runs");
            }
            if (!_entityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires a Valid Controlled EntityId before update runs");
            }
            var moveInput = _inputActionGateway.ReadMove();
            var DirX = moveInput.x;
            var DirY = moveInput.y;
            _commandBuffer.Enqueue(new MoveCommand(_entityId, DirX, DirY));
            if (_inputActionGateway.WasAttackPressedThisFrame())
            {
                _commandBuffer.Enqueue(new AttackCommand(_entityId, false, 0f));
            }
            if (_inputActionGateway.WasInteractPressedThisFrame())
            {
                _commandBuffer.Enqueue(new InteractCommand(_entityId));
            }
        }
        public void SwitchContext(InputContext targetContext)
        {
            if (_commandBuffer == null)
            {
                throw new InvalidOperationException("InputReader requires a CommandBuffer before SwitchContext");
            }
            var gateway = EnsureGateway();
            if (gateway.CurrentContext == targetContext)
            {
                return;
            }
            gateway.DisableCurrentContext();
            gateway.SetContext(targetContext);
            gateway.EnableCurrentContext();
            _commandBuffer.clear();
        }
        public bool WasSubmitPressedThisFrame()
        {
            var gateway = EnsureGateway();
            if (gateway.CurrentContext != InputContext.UI)
            {
                return false;
            }
            return gateway.WasSubmitPressedThisFrame();
        }
    }
}
