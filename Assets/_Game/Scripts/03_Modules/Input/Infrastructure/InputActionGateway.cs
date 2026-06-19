using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Tung.Modules.Input.Context;
using Tung.SharedPorts.Input;
namespace Tung.Modules.Input.Infrastructure
{
    public sealed class InputActionGateway
    {
        private readonly InputActionMap _playerActionMap;
        private readonly InputAction _moveAction;
        private readonly InputAction _attackAction;
        private readonly InputAction _interactAction;
        private readonly InputActionMap _uiActionMap;
        private readonly InputAction _submitAction;
        public InputContext? CurrentContext { get; private set; }
        public InputActionGateway(InputActionAsset action)
        {
            if (action == null)
            {
                throw new("InputActionGateway requires an InputActionAsset.");
            }
            _playerActionMap = action.FindActionMap(InputContextNames.Maps.Player, throwIfNotFound: true);
            _moveAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Move, throwIfNotFound: true);
            _attackAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Attack, throwIfNotFound: true);
            _interactAction = _playerActionMap.FindAction(InputContextNames.PlayerActions.Interact, throwIfNotFound: true);
            _uiActionMap = action.FindActionMap(InputContextNames.Maps.UI, throwIfNotFound: true);
            _submitAction = _uiActionMap.FindAction(InputContextNames.UiActions.Submit, throwIfNotFound: true);
        }
        public void SetContext(InputContext context)
        {
            switch (context)
            {
                case InputContext.Player: CurrentContext = InputContext.Player; return;
                case InputContext.UI: CurrentContext = InputContext.UI; return;
                case InputContext.Vehicle:
                default: throw new InvalidOperationException($"InputActionGateway does not support {context} ");
            }
        }
        public void EnableCurrentContext()
        {
            GetCurrentActionMap().Enable();
        }
        public void DisableCurrentContext()
        {
            GetCurrentActionMap().Disable();
        }
        public InputActionMap GetCurrentActionMap()
        {
            if (CurrentContext == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an active InputContext");
            }
            return CurrentContext switch
            {
                InputContext.Player => _playerActionMap,
                InputContext.UI => _uiActionMap,
                InputContext.Vehicle => throw new InvalidOperationException($"do not support {CurrentContext}"),
                _ => throw new InvalidOperationException($"do not support {CurrentContext}")
            };
        }
        public void EnsurePlayerContext()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException($"InputActionGateway requires an PlayerContext, but CurrentContext is {CurrentContext}");
            }
        }
        public Vector2 ReadMove()
        {
            EnsurePlayerContext();
            return _moveAction.ReadValue<Vector2>();
        }
        public bool WasAttackPressedThisFrame()
        {
            EnsurePlayerContext();
            return _attackAction.WasPressedThisFrame();
        }
        public bool WasInteractPressedThisFrame()
        {
            EnsurePlayerContext();
            return _interactAction.WasPressedThisFrame();
        }
        public void EnsureUiContext()
        {
            if (CurrentContext != InputContext.UI)
            {
                throw new InvalidOperationException($"InputActionGateway requires an UIContext, but CurrentContext is {CurrentContext}");
            }
        }
        public bool WasSubmitPressedThisFrame()
        {
            EnsureUiContext();
            return _submitAction.WasPressedThisFrame();
        }
    }
}