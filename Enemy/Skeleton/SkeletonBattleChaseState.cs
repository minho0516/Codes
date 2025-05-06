using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonBattleChaseState : EntityState
    {
        private float _moveDirection;
        private EnemySkeleton _enemy;
        private EntityMover _mover;
        private EnemyAttackCompo _attackCompo;
        public SkeletonBattleChaseState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = _enemy.GetCompo<EntityMover>();
            _attackCompo = _enemy.GetCompo<EnemyAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.IsBattleState = true;
            SetDirectionToEnemy();
        }

        public override void Update()
        {
            base.Update();
            Vector3 playerPos = _enemy.playerManager.PlayerTrm.position;
            _moveDirection = playerPos.x > _enemy.transform.position.x ? 1 : -1;

            _mover.SetMovement(_moveDirection);

            if (!_mover.IsGrounded || _attackCompo.IsInAttackDistance(playerPos))
            {
                _enemy.ChangeState(FSMState.BattleIdle);
                return;
            }

            if (_attackCompo.IsRunawayDistance(playerPos))
            {
                _enemy.ChangeState(FSMState.Idle);
            }
        }

        private void SetDirectionToEnemy()
        {
            float playerPosX = _enemy.playerManager.PlayerTrm.position.x;

            _renderer.FlipController(playerPosX - _enemy.transform.position.x);
        }
    }
}
