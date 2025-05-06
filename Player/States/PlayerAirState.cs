using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public abstract class PlayerAirState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        protected EntityAttackCompo _attackCompo;

        protected PlayerAirState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
            _attackCompo = entity.GetCompo<EntityAttackCompo>(); 
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SpeedMultiprlier = 0.7f;
            _player.PlayerInput.JumpEvent += HandleJump;
            _player.PlayerInput.AttackEvent += HandleJumpAttack;
        }

        private void HandleJump()
        {
            if(_player.CanJump)
            {
                _player.ChangeState(FSMState.Jump);
            }
        }

        private void HandleJumpAttack()
        {
            if(_attackCompo.CanJumpAttack()) _player.ChangeState(FSMState.JumpAttack);

        }

        public override void Exit()
        {
            Debug.Log("에어스테이트 탈주");
            _mover.SpeedMultiprlier = 1f;
            _player.PlayerInput.JumpEvent -= HandleJump;
            _player.PlayerInput.AttackEvent -= HandleJumpAttack;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            float xInput = _player.PlayerInput.InputDirection.x;

            if (Mathf.Abs(xInput) > 0)
            {
                _mover.SetMovement(xInput);
            }

            if(_mover.IsWallDitected())
            {
                _player.ChangeState(FSMState.WallSlide);
            }
        }
    }
}
