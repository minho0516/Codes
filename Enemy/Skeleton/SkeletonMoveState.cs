using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonMoveState : SkeletonGroundState
    {
        private EntityMover _mover;
        private float _stateTimer;

        public SkeletonMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _stateTimer = Time.time;
        }

        public override void Update()
        {
            _mover.SetMovement(_renderer.FacingDirection);

            if(_mover.IsWallDitected() || _mover.IsGrounded == false || _stateTimer + _enemy.moveTime < Time.time)
            {
                _enemy.ChangeState(FSMState.Idle);
                return;
            }
            base.Update(); //지속적인 상태전이 막기
        }

    }
}
