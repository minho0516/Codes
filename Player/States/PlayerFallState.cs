using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if(_mover.IsGrounded)
            {
                _player.ResetJumpCount(); //���� ī��Ʈ �ʱ�ȭ ���ֱ�
                _player.ChangeState(FSMState.Idle);
            }
        }
    }
}
