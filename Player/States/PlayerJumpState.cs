using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.StatSystem;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerJumpState : PlayerAirState
    {
        private EntityStat _stat;
        public PlayerJumpState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _stat = entity.GetCompo<EntityStat>();
        }

        public override void Enter()
        {
            base.Enter();
            StatSO jumpPowerStat = _stat.GetStat(_player.JumpPowerStat);
            Debug.Assert(jumpPowerStat != null, "JumpPowerStat is NULL"); //빌드할때 빠짐 방어적코딩 참이면 지나감 거짓이면 밑에코드도 안실행되고 다 멈춤

            _player.DecreaseJumpCount(); //점프카운트 감소
            _mover.StopImmediately(true); //점프한번 더할때 를 위한

            Vector2 power = new Vector2(0, jumpPowerStat.Value);
            _mover.AddForceToEntity(power);
        }

        public override void Update()
        {
            base.Update();
            Debug.Log(_mover.Velocity.x);
            if(_mover.Velocity.y <= 0) //히깅 페이즈로 전환된 거임
            {
                _player.ChangeState(FSMState.Fall);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
