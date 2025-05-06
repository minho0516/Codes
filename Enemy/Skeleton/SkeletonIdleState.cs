using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonIdleState : SkeletonGroundState
    {
        private EntityMover _mover;

        private float _stateTimer;

        public SkeletonIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
            _stateTimer = Time.time;
        }

        public override void Update()
        {
            base.Update();
            if(_stateTimer + _enemy.idleTime < Time.time)
            {
                _enemy.ChangeState(FSMState.Move);
            }
        }

        public override void Exit()
        {
            if(_mover.IsWallDitected() || _mover.IsGrounded == false)
            {
                _renderer.Flip();
                _mover.CheckGround();
            }
            base.Exit();
        }
    }
}
