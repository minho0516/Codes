using RPG.Combat;
using RPG.StatSystem;
using System;
using UnityEngine;

namespace RPG.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInitable, IDamageable
    {
        public float maxHealth;
        [SerializeField] public float _currentHealth;

        public event Action<Vector2> OnKnockbackEvent;

        private Entity _entity;
        private EntityStat _statSystem;
        private EntityFeedbackData _feedbackData;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statSystem = _entity.GetCompo<EntityStat>();
            _feedbackData = _entity.GetCompo<EntityFeedbackData>();
        }

        public void AfterInit()
        {
            _currentHealth = maxHealth = _statSystem.HpStat.Value;
        }

        public void ApplyDamage(float damage, Vector2 direction, Vector2 knockbackPower,bool isPowerAttack, Entity dealer)
        {
            if (_entity.IsDead) return;

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
            _feedbackData.LastAttackDirection = direction.normalized;
            _feedbackData.IsLastHitPowerAttack = isPowerAttack; //파워어택인지 기록
            _feedbackData.LastEntityWhoHit = dealer; //누가 때렸다. 기록

            AfterHitFeedbacks(knockbackPower);
        }

        private void AfterHitFeedbacks(Vector2 knockbackPower)
        {
            _entity.OnHitEvent?.Invoke();
            OnKnockbackEvent?.Invoke(knockbackPower);

            if(_currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }
        }
    }
}
