using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonStunState : EntityState
    {
        private EnemySkeleton _enemy;
        private EntityMover _mover;

        private float _stunStartTime;

        public SkeletonStunState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.FreezeStateChange = true; //상태 잠금
            _mover.CanManualMove = false;
            _stunStartTime = Time.time;
        }

        public override void Update()
        {
            base.Update();
            if (_stunStartTime + _enemy.stunTime < Time.time)
            {
                _enemy.FreezeStateChange = false;
                _enemy.ChangeState(FSMState.Battle);
            }
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            base.Exit();
        }
    }
}
