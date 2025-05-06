using RPG.Entities;
using UnityEngine;

namespace RPG.SkillSystem
{
    public delegate void CooldownInfoEvent(float current, float totalTime);
    public abstract class Skill : MonoBehaviour
    {
        public bool skillEnabled;

        [SerializeField] protected float _cooldown;

        public bool IsCooldown => _cooldownTimer > 0;

        protected float _cooldownTimer;
        protected Entity _entity;
        protected SkillComponent _skillCompo;

        public event CooldownInfoEvent OnCooldownEvent;

        [Header("Cooldown Info")]
        public SkillDataSO skillData;

        public virtual void InitializeSkill(Entity enttiy, SkillComponent skillCompo)
        {
            _entity = enttiy;
            _skillCompo = skillCompo;
        }

        protected virtual void Update()
        {
            if(_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                if(_cooldownTimer <= 0)
                {
                    _cooldownTimer = 0;
                }
                OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
            }
        }

        public virtual bool AttempUseSkill()
        {
            if(_cooldownTimer <= 0 && skillEnabled)
            {
                _cooldownTimer = _cooldown;
                UseSkill();
                return true;
            }
            Debug.Log("스킬 쿨다운 오어 락크드");
            return false;
        }

        public virtual void UseSkill()
        {

        }
    }
}
