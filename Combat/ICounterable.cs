using RPG.Entities;
using UnityEngine;

namespace RPG.Combat
{
    public interface ICounterable
    {
        public bool CanCounter { get; }
        public void ApplyCounter(float damage, Vector2 direction, Vector2 knockback, bool isPowerAttack, Entity dealer);
    }
}
