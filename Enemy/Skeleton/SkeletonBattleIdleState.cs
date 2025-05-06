using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class SkeletonBattleIdleState : EntityState
    {
        private EnemySkeleton _enemy;
        private EntityMover _mover;
        private EnemyAttackCompo _atkCompo;
        public SkeletonBattleIdleState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _enemy = entity as EnemySkeleton;
            _mover = entity.GetCompo<EntityMover>();
            _atkCompo = entity.GetCompo<EnemyAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }

        public override void Update()
        {
            base.Update();

            Vector3 playerPos = _enemy.playerManager.PlayerTrm.position;

            _renderer.FlipController(Mathf.Sign(playerPos.x - _enemy.transform.position.x));

            if(_mover.IsGrounded && _atkCompo.IsInAttackDistance(playerPos) == false)
            {
                _enemy.ChangeState(FSMState.Battle); //추적으로전환
                return;
            }

            if(_atkCompo.IsInAttackDistance(playerPos))
            {
                _enemy.ChangeState(FSMState.Attack);
                return;
            }
        }
    }
}
