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
    [SerializeField] GameObject  _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _crosshair;
    [Space]
    [SerializeField] private Transform _target;
    
    private LineRenderer _lineRenderer;
    private float _timer;
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_type == WeaponType.Bullet && _tier == WeaponTier.Tier1)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                BulletFire();
                _timer = 0;
            }
            else if (_type == WeaponType.Laser && _tier == WeaponTier.Tier1)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                LaserFire();
                _timer = 0;
            }
        }
        
        if (Input.GetMouseButton(1))
        {
            if (_type == WeaponType.Bullet && _tier == WeaponTier.Tier2)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                BulletFire();
                _timer = 0;
            }
            else if (_type == WeaponType.Laser && _tier == WeaponTier.Tier2)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                LaserFire();
                _timer = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            if (_type == WeaponType.Bullet && _tier == WeaponTier.Tier3)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                BulletFire();
                _timer = 0;
            }
            else if (_type == WeaponType.Laser && _tier == WeaponTier.Tier3)
            {
                _timer += Time.deltaTime;
                if (_timer < 1/ _fireRate) return;
                LaserFire();
                _timer = 0;
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Space))
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.SetPosition(0, _bulletSpawner.position);
            _lineRenderer.SetPosition(1, _bulletSpawner.position);
        }
    }

    private void BulletFire()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit, _range))
            targetpoint = hit.point;
        else
            targetpoint = ray.GetPoint(_range);
        
        Vector3 fireDirection = (targetpoint - _bulletSpawner.position).normalized;
        
        GameObject bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);
        bullet.GetComponent<Bullet>()._bulletDamage = _damage;
        
        bullet.transform.forward = fireDirection;
        bullet.GetComponent<Rigidbody>().AddForce(fireDirection * _bulletSpeed, ForceMode.Impulse);
    }

    private void LaserFire()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit, _range))
        {
            targetpoint = hit.point;
            _target = hit.transform;
            Enemy enemy = _target.GetComponentInParent<Enemy>();
            //Enemy enemy = _target.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(_damage);
        }
        else
            targetpoint = ray.GetPoint(_range);
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, _bulletSpawner.position);
        _lineRenderer.SetPosition(1, targetpoint);
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



