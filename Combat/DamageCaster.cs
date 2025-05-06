using RPG.Entities;
using UnityEngine;

namespace RPG.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int _maxHitCount;
        [SerializeField] protected ContactFilter2D _contactFilter;
        protected Collider2D[] _hitResults;

        protected Entity _owner;

        public virtual void InitCaster(Entity entity)
        {
            _hitResults = new Collider2D[_maxHitCount];
            _owner = entity;
        }

        public abstract bool CastDamage(float damage, Vector2 knockBack, bool isPowerAttack);
        public abstract ICounterable GetCounterableTarget(LayerMask whatIsCounterable);
    }
}
