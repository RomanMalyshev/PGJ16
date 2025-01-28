using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _range;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetpoint;
        
        if (Physics.Raycast(ray, out hit, _range))
        {
            targetpoint = hit.point;
        }
        else
            targetpoint = ray.GetPoint(_range);
        
        
        GameObject enemy = Instantiate(_enemy, targetpoint, Quaternion.identity);
    }
}
