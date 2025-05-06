using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonHitState : EntityState
    {
        private EnemySkeleton _enemy;
        private EntityMover _mover;
        public SkeletonHitState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall && _mover.IsGrounded) //¶¥¿¡´ê¾ÒÀ»¶§¸¸
            {
                _enemy.ChangeState(FSMState.Battle);
            }
        }
    }
}
