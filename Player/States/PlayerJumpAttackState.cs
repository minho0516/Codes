using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerJumpAttackState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private EntityAttackCompo _atkCompo;
        public PlayerJumpAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
            _atkCompo = entity.GetCompo<EntityAttackCompo>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(true);
            _mover.SetGravityMultiplier(0.2f); //살짝 떠서 

            _mover.CanManualMove = false;

            SetAttackData();
        }

        private void SetAttackData()
        {
            AttackDataSO atkData = _atkCompo.GetAttackData("PlayerJumpAttack");
            Vector2 movement = atkData.movement;
            movement.x *= _renderer.FacingDirection;
            _mover.AddForceToEntity(movement);

            _atkCompo.SetAttackData(atkData);
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
            {
                _player.ChangeState(FSMState.Fall);
            }
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            _mover.SetGravityMultiplier(1f);
            base.Exit();
        }
    }

    //함수의 기본은 내가 실행되고 나서는 실행되기 전으로 돌아간다.
}
