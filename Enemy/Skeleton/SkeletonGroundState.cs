using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.Players;
using UnityEngine;

namespace RPG.Enemies
{
    public abstract class SkeletonGroundState : EntityState
    {
        protected EnemySkeleton _enemy;
        protected Player _player;
        protected SkeletonGroundState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _player = _enemy.playerManager.Player;
        }

        public override void Update()
        {
            base.Update();

            RaycastHit2D hit = _enemy.IsPlayerDetected();

            float distance = Vector2.Distance(_enemy.transform.position, _player.transform.position);
            float closeDistance = 2f;

            if(distance < closeDistance || (hit && _enemy.IsObstacleInLine(hit.distance) == false))
            {
                _enemy.ChangeState(FSMState.React);
            }
        }
    }
}
