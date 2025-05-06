using System;
using DG.Tweening;
using RPG.Animators;
using RPG.Players;
using UnityEngine;

namespace RPG.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        public float FacingDirection { get; private set; } = 1;
        
        private Entity _entity;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);

        public void FadeSprite(float time, Action CompleteCallback = null)
        {
            _spriteRenderer.DOFade(0, time).OnComplete(() => CompleteCallback?.Invoke());
        }

        #region FlipControl

        public void Flip()
        {
            FacingDirection *= -1;
            _entity.transform.Rotate(0, 180f, 0);
        }

        public void FlipController(float xMove)
        {
            if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
                Flip();
        }
        
        #endregion

        
    }
}
