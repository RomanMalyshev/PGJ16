using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    public Transform _playerStartPoint;
    public int EnemiesCount;
    
    void Start()
    {
        EnemiesCount = _enemies.Count;
    }
    
    public void EnemyDies()
    {
        EnemiesCount--;
    }
}
