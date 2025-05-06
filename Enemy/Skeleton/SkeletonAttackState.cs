using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonAttackState : EntityState
    {
        private EnemySkeleton _enemy;
        private EntityMover _mover;
        public SkeletonAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
            Debug.Log("Attack");
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
            {
                _enemy.ChangeState(FSMState.Battle);
            }
        }

        public override void Exit()
        {
            _enemy.SetCounterWindowStatus(false);
            base.Exit();
        }
    }
}
