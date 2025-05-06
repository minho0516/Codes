using UnityEngine;

namespace RPG.Enviroment
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private float _parallaxOffset;

        private SpriteRenderer _spriteRenderer;
        private Material _material;

        private float _currentScroll;
        private float _ratio;
        private Transform _mainCamTrm;
        private float _beforePosition;

        private readonly int _offsetHash = Shader.PropertyToID("_Offset");

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _material = _spriteRenderer.material;
            _currentScroll = 0;
            _ratio = 1f / _spriteRenderer.bounds.size.x;

            _mainCamTrm = Camera.main.transform;
        }

        private void Start()
        {
            _beforePosition = _mainCamTrm.position.x;
        }

        private void LateUpdate()
        {
            float delta = _mainCamTrm.position.x - _beforePosition;

            _beforePosition = _mainCamTrm.position.x; //이전위치 갱신
            _currentScroll += delta * _parallaxOffset * _ratio;
            _material.SetVector(_offsetHash, new Vector2(_currentScroll, 0));
        }
    }
}
