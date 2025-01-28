using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private Image _bar;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;
    [Space]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Transform _homeSpot;
    [SerializeField] private float _patrolDistance = 50f;
    [SerializeField] private float _sightDistance = 100f;
    [SerializeField] private float _chaseDistance = 150f;
    [Space]
    [SerializeField] EnemyWeapon _weapon;
    [SerializeField] Transform _enemyCity;
    [Space]
    [SerializeField] private bool _isPatrol;
    [SerializeField] private bool _isGoHome;
    [SerializeField] private bool _isAttacking;
    [Space]
    [SerializeField] private float DistanceToPlayer;
    [SerializeField] private float DistanceToHomeSpot;
    [SerializeField] private Transform _target;
    private float health;
    
    private void Start()
    {
        health = _health;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        DistanceToPlayer = Vector3.Distance(_target.position, _enemyCity.position);
        DistanceToHomeSpot = Vector3.Distance(_enemyCity.position, _homeSpot.position);
        
        if (_health <= 0)
            Destroy(this.gameObject);
        
        if ((Vector3.Distance(_homeSpot.position, _enemyCity.position) > _chaseDistance) &&  (Vector3.Distance(_enemyCity.position, _target.position) > _attackRange))
        {
            _isGoHome = true;
            _isAttacking = false;
            _isPatrol = false;
        } 
        else if (Vector3.Distance(_enemyCity.position, _target.position) <= _sightDistance)
        {
            _isAttacking = true;
            _isPatrol = false;
            _isGoHome = false;
        }
        else if (Vector3.Distance(_homeSpot.position, _enemyCity.position) <= _patrolDistance)
        {
            _isPatrol = true;
            _isGoHome = false;
            _isAttacking = false;
        }

        if (_isPatrol)
            Patrol();
        else if (_isAttacking)
            Attack();
        else if (_isGoHome)
            GoingHome();
    }

    private void Patrol()
    {
     
    }

    private void Attack()
    {
        _enemyCity.transform.LookAt(_target);
        
        if (Vector3.Distance(_enemyCity.position, _target.position) > _attackRange)
        {
            _enemyCity.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        }
        else
        {
            _weapon.GetComponent<EnemyWeapon>().Attack(_target);
        }
    }

    private void GoingHome()
    {
        _enemyCity.transform.LookAt(_homeSpot);
        _enemyCity.Translate(Vector3.forward * _speed * Time.deltaTime * _speed * Time.deltaTime, Space.Self);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _bar.fillAmount = _health / health;        
    }
}
