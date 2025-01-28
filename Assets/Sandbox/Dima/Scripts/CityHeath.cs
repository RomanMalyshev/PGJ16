using UnityEngine;
using UnityEngine.UI;

public class CityHeath : MonoBehaviour
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
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _bar.fillAmount = _health / health;        
    }
}
