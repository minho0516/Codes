using RPG.Feedbacks;
using System.Collections;
using UnityEngine;

namespace RPG
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _delaySecond;
        [SerializeField] private float _blinkValue;

        private readonly int _blinkShaderParam = Shader.PropertyToID("_BlinkValue");

        private Material _blinkMaterial;
        private Coroutine _delayCoroutine;

        private void Awake()
        {
            _blinkMaterial = _spriteRenderer.material;
        }

        public override void CreateFeedback()
        {
            FinishFeedback();
            _blinkMaterial.SetFloat(_blinkShaderParam, _blinkValue);
            _delayCoroutine = StartCoroutine(SetToNormalAfterDelay());
        }

        private IEnumerator SetToNormalAfterDelay()
        {
            yield return new WaitForSeconds(_delaySecond);
            FinishFeedback();
        }

        public override void FinishFeedback()
        {
            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);

            _blinkMaterial.SetFloat(_blinkShaderParam, 0);
        }
    }
}
