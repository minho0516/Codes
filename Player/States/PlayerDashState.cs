using DG.Tweening;
using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.Players;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerDashState : EntityState
    {
        private Player _player;
        private EntityMover _mover;

        private readonly float _dashDistance = 4.5f, _dashTime = 0.25f;
        public PlayerDashState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            Vector2 playerInput = _player.PlayerInput.InputDirection;
            Vector2 dashDirection = playerInput.magnitude > 0.05f ? playerInput : _renderer.transform.right;

            _mover.CanManualMove = false;
            _mover.SetGravityMultiplier(0f);
            _mover.StopImmediately(true);

            Vector3 destination = _player.transform.position + (Vector3)dashDirection * (_dashDistance-0.5f);
            float dashTime = _dashTime;

            float distance = _dashDistance;
            if(_mover.CheckColliderInFront(dashDirection, ref distance))
            {
                destination = _player.transform.position + (Vector3)dashDirection * (distance - 0.5f);
                dashTime = distance * _dashTime / _dashDistance;
            }

            _player.transform.DOMove(destination, dashTime).SetEase(Ease.OutQuad).OnComplete(EndDash);
        }

        public override void Exit()
        {
            _mover.StopImmediately(false);
            _mover.CanManualMove = true;
            _mover.SetGravityMultiplier(1f);
            base.Exit();
        }

        private void EndDash()
        {
            _player.ChangeState(FSMState.Idle);
        }
    }
}
