using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event System.Action OnPlayerTakeDamage = default;
    public event System.Action OnEnemyTakeDamage = default;

    private bool pause;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    public void PlayerTakeDamage()
    {
        OnPlayerTakeDamage?.Invoke();
    }
    public void EnemyTakeDamage()
    {
        OnEnemyTakeDamage?.Invoke();
    }
}
