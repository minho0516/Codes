using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public abstract class PlayerGroundState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        public PlayerGroundState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.JumpEvent += HandleJumpEvent;
            _player.PlayerInput.AttackEvent += HandleAttackEvent;
            _player.PlayerInput.SlideEvent += HandleSlideEvent;
            _player.PlayerInput.CounterAttackEvent += HandleCounterAttack;
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.JumpEvent -= HandleJumpEvent;
            _player.PlayerInput.AttackEvent -= HandleAttackEvent;
            _player.PlayerInput.SlideEvent -= HandleSlideEvent;
            _player.PlayerInput.CounterAttackEvent -= HandleCounterAttack;
        }

        public override void Update()
        {
            base.Update();
            if(_mover.IsGrounded == false)
            {
                _player.ChangeState(FSMState.Fall);
            }
        }

        private void HandleJumpEvent()
        {
            _player.ChangeState(FSMState.Jump);
        }
        protected virtual void HandleAttackEvent()
        {
            if(_mover.IsGrounded)
            {
                _player.ChangeState(FSMState.Attack);
            }
        }
        protected virtual void HandleSlideEvent()
        {

        }

        private void HandleCounterAttack()
        {
            _player.ChangeState(FSMState.CounterAttack);
        }
    }
}
