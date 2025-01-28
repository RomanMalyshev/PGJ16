using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponType _type;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] GameObject  _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [Space]
    [SerializeField] private Transform _target;
    
    private LineRenderer _lineRenderer;
    private float _timer;
   

    public void Attack(Transform target)
    {
        _target = target;
        
        if (_type == WeaponType.Bullet)
        {
            _timer += Time.deltaTime;
            if (_timer < 1/ _fireRate) return;
            BulletFire();
            _timer = 0;
        }
        else if (_type == WeaponType.Laser)
        {
            _timer += Time.deltaTime;
            if (_timer < 1/ _fireRate) return;
            LaserFire();
            _timer = 0;
        }
    }

    public void BulletFire()
    {
        Vector3 fireDirection = (_target.position - _bulletSpawner.position).normalized;
        
        GameObject bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);
        bullet.GetComponent<Bullet>()._bulletDamage = _damage;
        
        bullet.transform.forward = fireDirection;
        bullet.GetComponent<Rigidbody>().AddForce(fireDirection * _bulletSpeed, ForceMode.Impulse);
    }

    public void LaserFire()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, _bulletSpawner.position);
        _lineRenderer.SetPosition(1, _target.position);
    }
}




