using RPG.Combat;
using RPG.Entities;
using RPG.FSM;
using UnityEngine;

namespace RPG.Enemies
{
    public class EnemySkeleton : FSMEnemy, ICounterable
    {
        public float idleTime, moveTime;
        public float stunTime;

        private EntityHealth _healthCompo;
        private EntityMover _mover;

        public bool IsBattleState { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            _healthCompo = GetCompo<EntityHealth>();
            _mover = GetCompo<EntityMover>();

            _healthCompo.OnKnockbackEvent += HandleKnockback;

            GetCompo<EnemyAnimationTrigger>().CounterStatusChange += SetCounterWindowStatus;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _healthCompo.OnKnockbackEvent -= HandleKnockback;

            GetCompo<EnemyAnimationTrigger>().CounterStatusChange -= SetCounterWindowStatus;
        }
        private void Start()
        {
            _stateMachine.Initialize(FSM.FSMState.Idle);
        }

        protected override void HandleHitEvent()
        {
            if(IsDead) return;
            if (GetCompo<EntityFeedbackData>().IsLastHitPowerAttack)
            {
                ChangeState(FSMState.Hit);
                return;
            }

            if (IsBattleState == false) ChangeState(FSMState.Battle);
        }

        protected override void HandleDeadEvent()
        {
            base.HandleDeadEvent();
            if (IsDead) return;
            FreezeStateChange = false; //스턴 상태였다고 해도 사망은 이루어져야 한다.
            ChangeState(FSMState.Dead);
        }

        private void HandleKnockback(Vector2 force)
        {
            float knockbackTime = 0.5f;
            _mover.Knockback(force, knockbackTime);
        }

        public override void AnimationFinishTrigger()
        {
            _stateMachine.currentState.AnimationEndTrigger();
        }

        #region CounterLogic

        public bool CanCounter { get; private set; }

        public void SetCounterWindowStatus(bool isOpen)
        {
            CanCounter = isOpen;
        }
        public void ApplyCounter(float damage, Vector2 direction, Vector2 knockback, bool isPowerAttack, Entity dealer)
        {
            CanCounter = false; //무한 스턴 금지
            _healthCompo.ApplyDamage(damage, direction, knockback, isPowerAttack, dealer);
            ChangeState(FSMState.Stun);
            Debug.Log("<color=green>Counter sucess</color>");
        }

        #endregion
    }
}
