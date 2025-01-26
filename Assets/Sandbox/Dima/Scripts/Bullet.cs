using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletLifeTime = 3f;
    [SerializeField] public float _bulletDamage;
    
    private void Awake()
    {
        Destroy(gameObject, _bulletLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(_bulletDamage);
        
        Destroy(gameObject);
    }
}
