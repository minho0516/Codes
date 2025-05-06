using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerSlideState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerSlideState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.CanManualMove = false;
            float slidePower = 5f;
            _mover.AddForceToEntity(new Vector2(slidePower * _renderer.FacingDirection, 0));
        }

        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
            {
                _player.ChangeState(FSMState.Idle);
            }
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            base.Exit();
        }

    }
}
