using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private string _name = "Hobbits always surprise you.";

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AchievManager _achievManager;
    [SerializeField] private int _enemiesCount;

    void Start()
    {
        SpawnEnemy();
        _gameManager.OnEnemyTakeDamage += SpawnEnemy;
    }
    private void SpawnEnemy()
    {
        if (_enemiesCount != 0)
        {
            Instantiate(_enemyPrefab);
            _enemiesCount--;
        } else
        {
            _achievManager.AchievementDone(_name);
        }
    }
}
