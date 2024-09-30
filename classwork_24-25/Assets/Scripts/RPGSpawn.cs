using UnityEngine;

public class RPGSpawn : MonoBehaviour
{
    private GameObject[] _spawnPoints;

    [ContextMenu("Spawn")]

    private void Start()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("Pointer");
        if (_spawnPoints.Length > 0 )
        {
            int rndID = Random.Range(0, _spawnPoints.Length);
            transform.position = _spawnPoints[rndID].transform.position;
        }
    }
}
