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
        private TungEntityId _controlledEntityId;
        private InputActionGateway _inputActionGateway;
        public void SetControlledEntityId(TungEntityId entityId)
        {
            if (!entityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires a valid controlled entityId");
            }
            _controlledEntityId = entityId;
            Debug.Log($"[InputReader] {gameObject.name} has successfully connected to Entity ID: {entityId}. Starting key reading!");
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
        private void Awake()
        {
            if (_action == null)
            {
                Debug.Log($"[InputReader] missing InputActionAsset on {gameObject.name}", this);
                enabled = false;
                return;
            }
            if (_commandBuffer == null)
            {
                Debug.LogError($"[InputReader] missing CommandBuffer on {gameObject.name}", this);
                enabled = false;
                return;
            }
            _inputActionGateway = EnsureGateway();
            _inputActionGateway.SetContext(InputContext.Player);
            Debug.LogWarning($"[InputReader] {gameObject.name} has finished initialization and is waiting to pass EntityId...", this);
        }
        private void OnEnable()
        {
            if (_inputActionGateway != null)
            {
                _inputActionGateway.EnableCurrentContext();
            }

        }
        private void OnDisable()
        {
            if (_inputActionGateway != null)
            {
                _inputActionGateway.DisableCurrentContext();
            }
        }
        [Inject]
        public void Inject(CommandBuffer commandbuffer)
        {
            _commandBuffer = commandbuffer ?? throw new ArgumentNullException(nameof(commandbuffer));
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
            if (!_controlledEntityId.IsValid)
            {
                return;
            }
            var moveInput = _inputActionGateway.ReadMove();
            var DirX = moveInput.x;
            var DirY = moveInput.y;
            _commandBuffer.Enqueue(new MoveCommand(_controlledEntityId, DirX, DirY));
            if (_inputActionGateway.WasAttackPressedThisFrame())
            {
                _commandBuffer.Enqueue(new AttackCommand(_controlledEntityId, false, 0f));
            }
            if (_inputActionGateway.WasInteractPressedThisFrame())
            {
                _commandBuffer.Enqueue(new InteractCommand(_controlledEntityId));
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
        public void OnDestroy()
        {
            if (_inputActionGateway != null)
            {
                _inputActionGateway.Dispose();
                _inputActionGateway = null;
            }
        }


    }
}
