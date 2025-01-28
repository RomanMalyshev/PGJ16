using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float _bulletDamage;
    [SerializeField] public Rigidbody _rigidbody;
    public void Init(float destroyTime)
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CityHeath player = collision.collider.GetComponentInParent<CityHeath>();
        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
        //Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(_bulletDamage);
        if (player != null)
            player.TakeDamage(_bulletDamage);
        
        Bullet bullet = collision.collider.GetComponent<Bullet>();
        if (bullet == null)
            Destroy(gameObject);
    }
}
