using RPG.Combat;
using RPG.Enemies;
using RPG.Entities;
using UnityEngine;

namespace RPG.Enemies
{
    public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
    {
        public float atkDistance;
        public float runAwayDistance;

        [SerializeField] private float _atkCooldown, _cooldownRandomness;
        private float _nextAtkTime;

        private Entity _entity;

        [Header("Reference")]
        [SerializeField] private DamageCaster _damageCaster;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _damageCaster.InitCaster(entity);
        }

        public void Attack()
        {
            _nextAtkTime = Time.time + _atkCooldown + Random.Range(-_cooldownRandomness, _cooldownRandomness);
        }

        public bool CanAttack() => _nextAtkTime < Time.time;

        public bool IsInAttackDistance(Vector3 targetPos) => GetDistanceToTarget(targetPos) <= atkDistance;
        public bool IsRunawayDistance(Vector3 targetPos) => GetDistanceToTarget(targetPos) > runAwayDistance;

        private float GetDistanceToTarget(Vector3 targetPos) => Vector2.Distance(targetPos, transform.position);
    }
}
