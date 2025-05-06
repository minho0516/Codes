using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private Vector3 _direction;
    private Rigidbody rigid;

    [Header("Speed")]
    [SerializeField] private float bulletSpeed = 5;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(transform.position + _direction * bulletSpeed * Time.fixedDeltaTime);

        if(BulletManager.Instance.IsInCircle(transform) == false)
        {
            BulletManager.Instance.Push(this); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleManager.Instance.CreateParticle();

        if(other.TryGetComponent(out Health health))
        {
            health.OnDamage(1);
        }

        BulletManager.Instance.Push(this);
    }
}
