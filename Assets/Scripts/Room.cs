using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    public bool isCleared = false;
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public Transform playerSpawnPoint;

    [HideInInspector] public List<GameObject> spawnedEnemies = new List<GameObject>();

    public void SpawnEnemies(GameObject enemyPrefab)
    {
        foreach (var point in enemySpawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, point.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    public void CheckCleared()
    {
        spawnedEnemies.RemoveAll(e => e == null);
        if (spawnedEnemies.Count == 0)
            isCleared = true;
    }
}