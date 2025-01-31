using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType _type;
    [SerializeField] private WeaponTier _tier;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] Bullet _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _crosshair;
    [Space] [SerializeField] private Transform _target;

    private LineRenderer _lineRenderer;
    private float _timerTier1;
    private float _timerTier2;
    private float _timerTier3;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (_type == WeaponType.Laser)
            _lineRenderer.SetPosition(0, _bulletSpawner.position);
        
        if ((Input.GetMouseButton(0)) && (_tier == WeaponTier.Tier1))
        {
                _timerTier1 += Time.deltaTime;
                if (_timerTier1 < 1 / _fireRate) return;
                Fire();
                _timerTier1 = 0;
        }

        if ((Input.GetMouseButton(1)) && (_tier == WeaponTier.Tier2))
        {
            _timerTier2 += Time.deltaTime;
            if (_timerTier2 < 1 / _fireRate) return;
            Fire();
            _timerTier2 = 0;
        }

        if ((Input.GetKey(KeyCode.Space)) &&(_tier == WeaponTier.Tier3))
        {
            _timerTier3 += Time.deltaTime;
            if (_timerTier3 < 1 / _fireRate) return;
            Fire();
            _timerTier3 = 0;
        }

        if (((!Input.GetMouseButton(0)) && (_tier == WeaponTier.Tier1)) || (!Input.GetMouseButton(1) && (_tier == WeaponTier.Tier2)) || (!Input.GetKey(KeyCode.Space)) && (_tier == WeaponTier.Tier3))
        {
            _lineRenderer.SetPosition(0, _bulletSpawner.position);
            _lineRenderer.SetPosition(1, _bulletSpawner.position);
        }
    }

    private void Fire()
    {
        Ray ray = _camera.ScreenPointToRay(_crosshair.rectTransform.position);
        RaycastHit hit;

        Vector3 targetpoint;

        if (Physics.Raycast(ray, out hit, _range))
        {
            targetpoint = hit.point;
            if (_type == WeaponType.Laser)
            {
                _target = hit.transform;
                Enemy enemy = _target.GetComponentInParent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(_damage);
            }
        }
        else
            targetpoint = ray.GetPoint(_range);

        if (_type == WeaponType.Laser)
        {
            _lineRenderer.SetPosition(0, _bulletSpawner.position);
            _lineRenderer.SetPosition(1, targetpoint);
        }
        else if (_type == WeaponType.Bullet)
        {
            Vector3 fireDirection = (targetpoint - _bulletSpawner.position).normalized;

            var bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);

            bullet.Init(_range / _bulletSpeed);
            bullet._bulletDamage = _damage;
            bullet.transform.forward = fireDirection;
            bullet._rigidbody.AddForce(fireDirection * _bulletSpeed, ForceMode.Impulse);
        }
    }
}

public enum WeaponType
{
    Bullet,
    Laser
}

public enum WeaponTier
{
    Tier1,
    Tier2,
    Tier3
}