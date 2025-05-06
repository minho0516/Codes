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
            Debug.Assert(jumpPowerStat != null, "JumpPowerStat is NULL"); //�����Ҷ� ���� ������ڵ� ���̸� ������ �����̸� �ؿ��ڵ嵵 �Ƚ���ǰ� �� ����

            _player.DecreaseJumpCount(); //����ī��Ʈ ����
            _mover.StopImmediately(true); //�����ѹ� ���Ҷ� �� ����

            Vector2 power = new Vector2(0, jumpPowerStat.Value);
            _mover.AddForceToEntity(power);
        }

        public override void Update()
        {
            base.Update();
            Debug.Log(_mover.Velocity.x);
            if(_mover.Velocity.y <= 0) //���� ������� ��ȯ�� ����
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
