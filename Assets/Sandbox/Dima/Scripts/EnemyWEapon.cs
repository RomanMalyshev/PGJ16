using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponType _type;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] Bullet _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [Space]
    [SerializeField] private Transform _target;
    
    private LineRenderer _lineRenderer;
    private float _timer;
    private float _range;
    private CityHeath _playerCityHeath;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Attack(Transform target, float range)
    {
        _target = target;
        _range = range;
        _playerCityHeath = _target.GetComponent<CityHeath>();
        
        _timer += Time.deltaTime;
        if (_timer < 1/ _fireRate) return;
        Fire();
        _timer = 0;
    }

    public void Fire()
    {
        if (_type == WeaponType.Bullet)
        {
            Vector3 fireDirection = (_target.position - _bulletSpawner.position).normalized;
        
            var bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);
            bullet.Init(_range/_bulletSpeed);
            bullet._bulletDamage = _damage;
        
            bullet.transform.forward = fireDirection;
            bullet._rigidbody.AddForce(fireDirection * _bulletSpeed, ForceMode.Impulse);
        }
        else if (_type == WeaponType.Laser)
        {
            _playerCityHeath.TakeDamage(_damage);
            _lineRenderer.SetPosition(0, _bulletSpawner.position);
            _lineRenderer.SetPosition(1, _target.position);
        }
    }
}




