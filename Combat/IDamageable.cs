using RPG.Entities;
using UnityEngine;

namespace RPG.Combat
{
    public interface IDamageable
    {
        public void ApplyDamage(float damage, Vector2 direction, Vector2 knockbackPower,bool isPowerAttack, Entity dealer);
    }
}
