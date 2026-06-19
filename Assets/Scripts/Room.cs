using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    public bool isCleared = false;
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public Transform playerSpawnPoint;

    [Header("Oda Sınırları")]
    public Vector2 roomMin = new Vector2(-4.5f, -4.5f);
    public Vector2 roomMax = new Vector2(4.5f, 4.5f);

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

    public Vector3 ClampToRoom(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, roomMin.x, roomMax.x);
        pos.y = Mathf.Clamp(pos.y, roomMin.y, roomMax.y);
        return pos;
    }
}