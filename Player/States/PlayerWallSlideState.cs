using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.Players;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerWallSlideState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerWallSlideState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.LimitYSpeed = 10f;
            _mover.StopImmediately(true);
            _mover.SetGravityMultiplier(0.3f);
        }

        public override void Exit()
        {
            _mover.LimitYSpeed = 40f;
            _mover.SetGravityMultiplier(1f);
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;

            if(Mathf.Abs(xInput + _renderer.FacingDirection) < 0.5f) // 반대 방향으로 눌렸다
            {
                _player.ChangeState(FSMState.Idle);
                return;
            }

            if(_mover.IsGrounded || !_mover.IsWallDitected())
            {
                _player.ChangeState(FSMState.Idle);
                _player.ResetJumpCount();
            }
        }
    }
}
