using System;
using System.Collections.Generic;
using RPG.Entities;
using RPG.EventSystem;
using RPG.FSM;
using RPG.Players;
using RPG.StatSystem;
using UnityEngine;
using StateMachine = RPG.FSM.StateMachine;

namespace RPG.Enemies
{
    public abstract class FSMEnemy : Entity
    {
        public PlayerManagerSO playerManager;
        public LayerMask whatIsPlayer, whatIsObstacle;

        public bool FreezeStateChange { get; set; } = false;

        [Header("References")]
        [SerializeField] protected Transform _frontCheckerTrm;

        [SerializeField] protected GameEventChannelSO _playerChannel;
        [SerializeField] protected int _dropExp;

        protected EntityRenderer _renderer;
        protected EnemyAttackCompo _atkCompo;

        protected StateMachine _stateMachine;

        public EntityStateListSO enemyFSM;
        public float FacingDirection => _renderer.FacingDirection;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetCompo<EntityRenderer>();
            _atkCompo = GetCompo<EnemyAttackCompo>();
            _stateMachine = new StateMachine(enemyFSM, this);
        }

        public virtual RaycastHit2D IsPlayerDetected()
            => Physics2D.Raycast(_frontCheckerTrm.position,
                Vector2.right * FacingDirection, _atkCompo.runAwayDistance, whatIsPlayer);

        public virtual bool IsObstacleInLine(float distance)
            => Physics2D.Raycast(_frontCheckerTrm.position, Vector2.right * FacingDirection,
                distance, whatIsObstacle);

        protected virtual void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(FSMState newState)
        {
            if (FreezeStateChange) return;

            _stateMachine.ChangeState(newState);
        }

        public EntityState GetState(FSMState state)
            => _stateMachine.GetState(state);

        public abstract void AnimationFinishTrigger();

        protected override void HandleDeadEvent()
        {
            base.HandleDeadEvent();
            AddExpEvent evt = PlayerEvents.AddExp;
            evt.exp = _dropExp;
            _playerChannel.RaiseEvent(evt);
        }

#if UNITY_EDITOR
        protected void OnDrawGizmos()
        {
            if (_renderer == null || _atkCompo == null) return;
            Gizmos.color = Color.yellow;
            Vector3 destination =
                new Vector3(transform.position.x + _atkCompo.atkDistance * FacingDirection, transform.position.y);
            Gizmos.DrawLine(transform.position, destination);
        }
#endif

    }
}