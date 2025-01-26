using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private Image _bar;
    private float health;
    
    private void Start()
    {
        health = _health;
    }

    private void Update()
    {
        if (_health <= 0)
            Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _bar.fillAmount = _health / health;        
    }
}
