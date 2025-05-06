using System;
using UnityEngine;

namespace RPG.Entities
{
    public class EntityAnimationTrigger : MonoBehaviour,IEntityComponent
    {
        public event Action OnAnimationEndTrigger;
        public event Action OnAttackTrigger;

        protected Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        protected virtual void AnimationEnd() => OnAnimationEndTrigger?.Invoke();
        protected virtual void AttackTrigger() => OnAttackTrigger?.Invoke();
    }
}
