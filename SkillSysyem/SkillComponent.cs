using RPG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.SkillSystem
{
    public class SkillComponent : MonoBehaviour, IEntityComponent
    {
        public ContactFilter2D whatIsEnemy;
        public Collider2D[] colliders;

        [SerializeField] protected int _maxCheckEnemy;
        private Entity _entity;

        private Dictionary<Type, Skill> _skills;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            colliders = new Collider2D[_maxCheckEnemy];

            _skills = new Dictionary<Type, Skill>();
            GetComponentsInChildren<Skill>().ToList().ForEach(skill => _skills.Add(skill.GetType(), skill));
            _skills.Values.ToList().ForEach(skill => skill.InitializeSkill(_entity, this));
        }

        public T GetSKill<T>() where T : Skill
        {
            Type type = typeof(T);
            if(_skills.TryGetValue(type, out Skill skill))
            {
                return (T)skill;
            }

            return default;
        }

        public virtual void GetEnemiesInRange(Transform checkTrm, float range)
            => Physics2D.OverlapCircle(checkTrm.position, range, whatIsEnemy, colliders);

        public virtual void GetEnemiesInRange(Vector3 checkPos, float range)
            => Physics2D.OverlapCircle(checkPos, range, whatIsEnemy, colliders);

        public virtual Transform FindClosestEnemy(Transform checkTrm, float range)
        {
            Transform closestEnemy = null;
            int cnt = Physics2D.OverlapCircle(checkTrm.position, range, whatIsEnemy, colliders);

            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < cnt; i++)
            {
                if (colliders[i].TryGetComponent(out Entity target))
                {
                    if (target.IsDead) continue;
                    float distanceToTarget = Vector2.Distance(checkTrm.position, target.transform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestEnemy = target.transform;
                    }
                }
            }

            return closestEnemy;
        }
    }
}
