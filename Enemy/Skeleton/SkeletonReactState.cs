using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonReactState : EntityState
    {
        private EnemySkeleton _enemy;
        private EntityMover _mover;
        private float _stateTimer;
        public SkeletonReactState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();


            Vector3 playerPos = _enemy.playerManager.PlayerTrm.position;
            Vector2 direction = playerPos - _enemy.transform.position;
            float directionX = Mathf.Sign(direction.x);

            if(Mathf.Abs(_renderer.FacingDirection + directionX) > 0.5f)
            {
                _renderer.Flip();
            }
            
            _stateTimer = Time.time;
        }

        public override void Update()
        {
            base.Update();
            float reactTime = 0.5f;
            if(_stateTimer + reactTime < Time.time)
            {
                _enemy.ChangeState(FSMState.Battle);
            }
        }
    }
}
