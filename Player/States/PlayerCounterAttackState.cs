using RPG.Animators;
using RPG.Combat;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Players
{
    public class PlayerCounterAttackState : EntityState
    {

        private Player _player;
        private EntityAttackCompo _attackCompo;
        private EntityMover _mover;

        //private Collider2D[] _counterHitResults = new Collider2D[1];
        private bool _counterSucess;
        private float _counterTimer;

        public PlayerCounterAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
        {
            _player = entity as Player;
            _attackCompo = _player.GetCompo<EntityAttackCompo>();
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(false);
            _counterTimer = _attackCompo.counterWindoSize;
            _renderer.SetParam(_attackCompo.sucessCounterParam, false);
            _counterSucess = false;
        }

        public override void Update()
        {
            base.Update();
            _counterTimer -= Time.deltaTime;
            if (_counterSucess == false)
            {
                CheckCounter();
            }

            if(_counterTimer <= 0 || _isTriggerCall)
            {
                _player.ChangeState(FSMState.Idle);
            }
        }

        private void CheckCounter()
        {
            ICounterable counterable = _attackCompo.GetCounterTarget();

            if (counterable != null && counterable.CanCounter)
            {
                
                _counterSucess = true;
                AttackDataSO atkData = _attackCompo.GetAttackData("PlayerCounterAttack");
                Vector2 atkDirection = new Vector2(_renderer.FacingDirection, 0);
                Vector2 knockback = atkData.knockbackForce;
                atkDirection.x *= _renderer.FacingDirection;
                float damage = _attackCompo.CaculateDamage(atkData);

                counterable.ApplyCounter(damage, atkDirection, knockback, atkData.isPowerAttack, _player);
                _counterTimer = 10f;
                _renderer.SetParam(_attackCompo.sucessCounterParam, true);
            }
        }
    }
}
