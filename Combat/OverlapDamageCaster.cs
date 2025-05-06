using UnityEngine;

namespace RPG.Combat
{
    public class OverlapDamageCaster : DamageCaster
    {
        public float damageRadius;
        public override bool CastDamage(float damage, Vector2 knockBack, bool isPowerAttack)
        {
            int cnt = Physics2D.OverlapCircle(transform.position, damageRadius, _contactFilter, _hitResults);

            for (int i = 0; i < cnt; i++)
            {
                Vector2 direction = (_hitResults[i].transform.position - _owner.transform.position).normalized;

                knockBack.x *= Mathf.Sign(direction.x);

                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage, direction, knockBack, isPowerAttack, _owner);
                }
            }

            return cnt > 0;
        }

        public override ICounterable GetCounterableTarget(LayerMask whatIsCounterable)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsCounterable);

            if (collider != null)
                return collider.GetComponent<ICounterable>();

            return default;
        }
    }
}
