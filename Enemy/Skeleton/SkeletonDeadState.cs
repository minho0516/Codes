using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonDeadState : EntityState
    {
        private EnemySkeleton _enemy;
        public SkeletonDeadState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.IsDead = true;
            _enemy.FreezeStateChange = true;

            float deadDelay = 3f;

            _renderer.FadeSprite(deadDelay, () =>
            {
                _renderer.FadeSprite(1f, () => GameObject.Destroy(_enemy.gameObject));
            });
        }
    }
}
