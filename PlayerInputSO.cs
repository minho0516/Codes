using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public event Action JumpEvent;
        public event Action DashEvent;
        public event Action AttackEvent;
        public event Action SlideEvent;
        public event Action CounterAttackEvent;

        public Vector2 InputDirection { get; private set; }

        private Controls _controls;

        public void ClearSubscription()
        {
            JumpEvent = null;
            DashEvent = null; 
            AttackEvent = null; 
            SlideEvent = null; 
            CounterAttackEvent = null;
        }
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls= new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                JumpEvent?.Invoke();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                DashEvent?.Invoke();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                AttackEvent?.Invoke();
            }
        }

        public void OnSlide(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                SlideEvent?.Invoke();
            }
        }

        public void OnCounter(InputAction.CallbackContext context)
        {
            if(context.performed)
                CounterAttackEvent?.Invoke();
        }
    }
}
