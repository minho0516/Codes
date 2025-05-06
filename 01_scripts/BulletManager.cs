using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    [SerializeField] private Transform _palyerTrm;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private float _genericRadius = 15f;
    [SerializeField] private float _generateTerm = 1f;

    private float _currentTime = 0;

    private Stack<Bullet> _bulletPool;



    int _wave = 1;

    private void Awake()
    {
        Instance = this;

        _bulletPool = new Stack<Bullet>(50);
        //if(Instance != null)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void CreateBullet()
    {
        if (_palyerTrm == null) return;

        Vector2 pos = Random.insideUnitCircle.normalized * _genericRadius;
        Vector3 bulletPos = new Vector3(pos.x, 0, pos.y);
        Bullet newBullet = null;

        if (_bulletPool.Count > 0)
        {
            newBullet = _bulletPool.Pop();
            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = bulletPos;
        }
        else
        {
            newBullet = Instantiate(_prefab, bulletPos, Quaternion.identity);
        }
        Vector3 targetPos = Vector3.zero;
        if(_palyerTrm != null)
        {
            targetPos = _palyerTrm.position;
        }

        Vector3 direction = targetPos - bulletPos;
        newBullet.SetDirection(direction.normalized);
        
    }

    public void Push(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Push(bullet);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime > _generateTerm && _palyerTrm != null)
        {
            _currentTime -= _generateTerm;

            StartCoroutine(DelayGenerateBullet(1 + _wave));

            _wave = Mathf.Clamp(_wave + 1, 0, 5);
        }
    }

    private IEnumerator DelayGenerateBullet(int count)
    {
        float delayTime = _generateTerm / count;
        for(int i = 0; i < count; i++)
        {
            CreateBullet();
            yield return new WaitForSeconds(delayTime);
        }
    }

    public bool IsInCircle(Transform trm)
    {
        float distance = Vector3.Distance(trm.position, Vector3.zero);
        return distance <= _genericRadius + 1f;
    }
}
