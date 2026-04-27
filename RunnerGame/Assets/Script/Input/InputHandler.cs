using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerInputHandler
{

    public class InputHandler : MonoBehaviour
    {
        PlayerInput _playerInput;

        InputAction _movementAction, _dashAction,_pauseAction;
        public InputValue _inputValue { get; private set; }


        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _movementAction = _playerInput.actions["Movement"];
            _dashAction = _playerInput.actions["Dash"];
            _pauseAction = _playerInput.actions["Pause"];
        }

        private void Update()
        {
           _inputValue =  GatherInput();
        }

         InputValue GatherInput()
        {
            return new InputValue()
            {
                _Movement = _movementAction.ReadValue<Vector2>(),
                _DashDown = _dashAction.IsPressed(),
                _DashUp = _dashAction.WasReleasedThisFrame(),
                _Pause = _pauseAction.WasPressedThisFrame(),
            };
        }
       
    }

    public struct InputValue
    {
        public Vector2 _Movement;
        public bool _DashDown;
        public bool _DashUp;
        public bool _Pause;
    }
}

