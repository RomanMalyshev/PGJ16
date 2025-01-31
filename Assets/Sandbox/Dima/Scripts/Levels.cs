using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private int _currentLevelNumber;
    [SerializeField] private GameObject _player;
    private Level _currentLevel;

    private void Start()
    {
        _currentLevelNumber = 0;
        _currentLevel = Instantiate(_levels[_currentLevelNumber]);
        _player.transform.position = _levels[_currentLevelNumber]._playerStartPoint.transform.position;
    }

    private void Update()
    {
        /*if (_levels[_currentLevelNumber].EnemiesCount == 0)
        {
            LevelWin();
        }*/
    }

    public void RetartLevel()
    {
        Destroy(_currentLevel);
        Instantiate(_levels[_currentLevelNumber]);
        _player.transform.position = _levels[_currentLevelNumber]._playerStartPoint.transform.position;
    }

    private void LevelWin()
    {
        _currentLevelNumber++;
        Destroy(_currentLevel);
        Instantiate(_levels[_currentLevelNumber]);
        _player.transform.position = _levels[_currentLevelNumber]._playerStartPoint.transform.position;
    }
}
