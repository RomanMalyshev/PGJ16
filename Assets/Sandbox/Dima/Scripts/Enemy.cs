using System.Collections.Generic;
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
    [SerializeField] private float _rotateSpeed = 400f;
    [SerializeField] private Transform _homeSpot;
    [SerializeField] private float _patrolDistance = 50f;
    [SerializeField] private float _sightDistance = 100f;
    [SerializeField] private float _chaseDistance = 150f;
    [Space]
    [SerializeField] List<EnemyWeapon> _weapons;
    [SerializeField] Transform _enemyCity;
    [Space]
    [SerializeField] private bool _isPatrol;
    [SerializeField] private bool _isGoHome;
    [SerializeField] private bool _isAttacking;
    [Space]
    [SerializeField] private float DistanceToPlayer;
    [SerializeField] private float DistanceToHomeSpot;
    [SerializeField] private Transform _target;
    [Space]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _enemyRotatePlatform;
    [SerializeField] private Canvas _canvas;   
    private float health;
    [SerializeField] private Vector3 _patrolPoint;
    [SerializeField] private bool _goToPatrolPoint = false;
    
    private void Start()
    {
        health = _health;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _canvas.transform.eulerAngles = Camera.main.transform.eulerAngles;
        DistanceToPlayer = Vector3.Distance(_target.position, _enemyCity.position);
        DistanceToHomeSpot = Vector3.Distance(_enemyCity.position, _homeSpot.position);
        
        if (_health <= 0)
            Destroy(this.gameObject);
        
        if ((Vector3.Distance(_homeSpot.position, _enemyCity.position) > _chaseDistance) &&  (Vector3.Distance(_enemyCity.position, _target.position) > _attackRange))
        {
            _isGoHome = true;
            _isPatrol = false;
        } 
        else if (Vector3.Distance(_homeSpot.position, _enemyCity.position) < _patrolDistance)
        {
            _isPatrol = true;
            _isGoHome = false;
        }

        if (Vector3.Distance(_enemyCity.position, _target.position) <= _sightDistance)
        {
            _isAttacking = true;
            _isPatrol = false;
        }
        else
        {
            _isAttacking = false;
        }
        
        if (!_isAttacking && (Vector3.Distance(_homeSpot.position, _enemyCity.position) > _patrolDistance))
            _isGoHome = true;

        if (_isPatrol)
            Patrol();
        else if (_isGoHome)
            GoingHome();
        
        if (_isAttacking)
            Attack();
    }

    private void Patrol()
    {
        if (!_goToPatrolPoint)
        {
            _goToPatrolPoint = true;
            _patrolPoint = transform.position + Random.insideUnitSphere * _patrolDistance;
        }
        else
        {
            var move = (new Vector3(_patrolPoint.x - _enemyCity.position.x, 0, _patrolPoint.z - _enemyCity.position.z)).normalized;
            _controller.Move(move * _speed * Time.deltaTime);
        }

        if (Mathf.Abs(_enemyCity.position.x - _patrolPoint.x) < 0.5 && Mathf.Abs(_enemyCity.position.z - _patrolPoint.z) < 0.5)
        {
            _goToPatrolPoint = false;
        }
    }

    private void Attack()
    {
        var move = (new Vector3(_target.position.x - _enemyCity.position.x, 0, _target.position.z - _enemyCity.position.z)).normalized;
        
        float Angle = -Mathf.Atan2(move.z, move.x)/Mathf.PI*180f;
        float RotAng = _rotateSpeed*Time.deltaTime;
        float DeltaAng = Mathf.DeltaAngle(_enemyRotatePlatform.eulerAngles.y, Angle);
        
        if (Mathf.Abs(DeltaAng) < RotAng)
            _enemyRotatePlatform.eulerAngles = new Vector3(_enemyRotatePlatform.eulerAngles.x, Angle, _enemyRotatePlatform.eulerAngles.z);
        else 
            _enemyRotatePlatform.eulerAngles += new Vector3(0, RotAng*Mathf.Sign(DeltaAng), 0);
        
        if (Vector3.Distance(_enemyCity.position, _target.position) > _attackRange)
        {
            _controller.Move(move * _speed * Time.deltaTime);
        }
        else
        {
            foreach (var weapon in _weapons)
            {
                weapon.Attack(_target, _attackRange);
            }
        }
    }

    private void GoingHome()
    {
        var move = (new Vector3(_homeSpot.position.x - _enemyCity.position.x, 0, _homeSpot.position.z - _enemyCity.position.z)).normalized;
        _controller.Move(move * _speed * Time.deltaTime);
        _goToPatrolPoint = false;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _bar.fillAmount = _health / health;        
    }
}
