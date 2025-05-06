using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerAttackState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private EntityAttackCompo _atkCompo;

        private int _comboCounter;
        private float _lastAtkTime;
        private float _comboWindow = 0.8f;

        private float _delayStop = 0.2f;
        private bool _isAtkStart = false;
        public PlayerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
            _atkCompo = _player.GetCompo<EntityAttackCompo>();
        }
        public override void Enter()
        {
            base.Enter();
            
            if(_comboCounter > 2 || Time.time >= _lastAtkTime + _comboWindow)
            {
                _comboCounter = 0;
            }
            _renderer.SetParam(_player.comboCountParam, _comboCounter);

            _mover.CanManualMove = false;
            _mover.StopImmediately(true);

            SetAttackData();
        }

        private void SetAttackData()
        {
            float atkDirection = _renderer.FacingDirection;
            float xInput = _player.PlayerInput.InputDirection.x;
            if (Mathf.Abs(xInput) > 0)
            {
                atkDirection = Mathf.Sign(xInput); //부호를 반환 0을넣어도 1이나옴
            }

            AttackDataSO atkData = _atkCompo.GetAttackData($"PlayerCombo{_comboCounter}");
            Vector2 movement = atkData.movement;
            movement.x *= atkDirection;
            _mover.AddForceToEntity(movement);

            _delayStop = 0.2f;
            _isAtkStart = true;

            _atkCompo.SetAttackData(atkData);
        }

        public override void Update()
        {
            base.Update();
            if(_isTriggerCall)
            {
                _player.ChangeState(FSMState.Idle);
            }
        }

        public override void Exit()
        {
            _mover.CanManualMove = true;
            ++_comboCounter;
            _lastAtkTime = Time.time;
            base.Exit();
        }
    }
}
