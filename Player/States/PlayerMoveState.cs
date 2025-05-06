using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        private float _enterTime;
        public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _enterTime = Time.time;
        }
        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            
            _mover.SetMovement(xInput);

            if (Mathf.Approximately(xInput, 0))
            {
                _player.ChangeState(FSMState.Idle);
            }
        }

        protected override void HandleAttackEvent()
        {
            float overDashTime = 0.3f;
            if(_enterTime + overDashTime < Time.time)
            {
                _player.ChangeState(FSMState.DashAttack);
            }
            else
            {
                base.HandleAttackEvent();
            }
        }

        protected override void HandleSlideEvent()
        {
            float overSlideTime = 0.5f;
            if(_enterTime + overSlideTime < Time.time)
            {
                _player.ChangeState(FSMState.Slide);
            }
        }
    }
}
