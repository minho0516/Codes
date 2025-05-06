using System;
using System.Collections.Generic;
using RPG.Animators;
using RPG.Entities;
using RPG.FSM;
using RPG.SkillSystem;
using RPG.StatSystem;
using UnityEngine;

namespace RPG.Players
{
    public class Player : Entity
    {
        public EntityStateListSO playerFSM;
        // public StateSO idleState, moveState;
        // [SerializeField] private List<StateSO> _stateList;

        [Header("Setting Value")] public Vector2[] atkMovement;
        
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        private StateMachine _stateMachine;

        [field:SerializeField] public StatSO JumpPowerStat { get; private set; }
        [field:SerializeField] public StatSO JumpCountStat { get; private set; }
        [field:SerializeField] public StatSO AttackSpeedStat { get; private set; }

        public AnimParamSO comboCountParam;

        private float _maxJumpCount;
        private float _currentJumpCount = 0;
        public bool CanJump => _currentJumpCount > 0;

        private EntityMover _mover;
        public Vector2 dashAttackMovement;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine(playerFSM, this);

        }

        protected override void AfterInitComponents()
        {
            base.AfterInitComponents();

            JumpCountStat = GetCompo<EntityStat>().GetStat(JumpCountStat);
            Debug.Assert(JumpCountStat != null, "jumpStat is NULL");

            JumpCountStat.OnValueChange += HandleJumpCountChange;

            _maxJumpCount = JumpCountStat.Value;
            ResetJumpCount(); //한번은 실행해줘야해

            PlayerInput.DashEvent += HandleDash;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            JumpCountStat.OnValueChange -= HandleJumpCountChange;
            PlayerInput.ClearSubscription();
        }
        private void HandleJumpCountChange(StatSO stat, float current, float previous)
        {
            _maxJumpCount = current;
            ResetJumpCount();
        }
        private void HandleDash()
        {
            if(GetCompo<SkillComponent>().GetSKill<DashSkill>().AttempUseSkill())
                ChangeState(FSMState.Dash);
        }

        private void Start()
        {
            _stateMachine.Initialize(FSMState.Idle);
        }
        public void ChangeState(FSMState stateName) => _stateMachine.ChangeState(stateName);

        public void DecreaseJumpCount() => _currentJumpCount--;
        public void ResetJumpCount() => _currentJumpCount = _maxJumpCount;

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
        public void AnimationFinishTrigger() => _stateMachine.currentState.AnimationEndTrigger();

        protected override void HandleHitEvent()
        {
             
        }

        protected override void HandleDeadEvent()
        {
            base.HandleDeadEvent();
        }
    }
}
