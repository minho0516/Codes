using RPG.Animators;
using RPG.Entities;

namespace RPG.FSM
{
    public abstract class EntityState
    {
        protected Entity _entity;
        
        protected AnimParamSO _animParam;
        protected bool _isTriggerCall;

        protected EntityRenderer _renderer;

        protected EntityAnimationTrigger _animationTrigger;

        public EntityState(Entity entity, AnimParamSO animParam)
        {
            _entity = entity;
            _animParam = animParam;
            _renderer = _entity.GetCompo<EntityRenderer>();
            _animationTrigger = _entity.GetCompo<EntityAnimationTrigger>(true); //��ӹ��� �ֵ���� �����ü��հ�
        }

        public virtual void Enter()
        {
            _renderer.SetParam(_animParam, true);
            _isTriggerCall = false;
            _animationTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
        }

        public virtual void Update() { }

        public virtual void Exit()
        {
            _renderer.SetParam(_animParam, false);
            _animationTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
        }

        public virtual void AnimationEndTrigger()
        {
            _isTriggerCall = true;
        }
    }
}
