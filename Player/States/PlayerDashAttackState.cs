using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.Players;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerDashAttackState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private EntityAttackCompo _atkCompo;
        public PlayerDashAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
            _atkCompo = entity.GetCompo<EntityAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            SetAttackData();
        }

        private void SetAttackData()
        {
            AttackDataSO atkData = _atkCompo.GetAttackData("PlayerDashAttack");
            Vector2 movement = atkData.movement;
            movement.x *= _renderer.FacingDirection;

            _mover.AddForceToEntity(movement);
            _atkCompo.SetAttackData(atkData);
        }
        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
                _player.ChangeState(FSMState.Idle);
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            base.Exit();
        }
    }
}
