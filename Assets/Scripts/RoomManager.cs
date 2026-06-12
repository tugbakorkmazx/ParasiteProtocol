using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject enemyPrefab;
    public Transform player;

    private Room currentRoom;
    private List<Room> spawnedRooms = new List<Room>();
    private int roomIndex = 0;

    void Start()
    {
        LoadRoom(roomIndex);
    }

    void Update()
    {
        if (currentRoom == null) return;

        currentRoom.CheckCleared();

        if (currentRoom.isCleared && Input.GetKeyDown(KeyCode.F))
        {
            roomIndex++;
            LoadRoom(roomIndex % roomPrefabs.Length);
        }
    }

    void LoadRoom(int index)
    {
        // Eski odayı temizle
        foreach (var room in spawnedRooms)
            if (room != null) Destroy(room.gameObject);
        spawnedRooms.Clear();

        // Yeni oda oluştur
        GameObject roomObj = Instantiate(roomPrefabs[index], Vector3.zero, Quaternion.identity);
        currentRoom = roomObj.GetComponent<Room>();
        spawnedRooms.Add(currentRoom);

        // Player'ı spawn noktasına taşı
        if (currentRoom.playerSpawnPoint != null)
            player.position = currentRoom.playerSpawnPoint.position;

        // Düşmanları spawn et
        currentRoom.SpawnEnemies(enemyPrefab);

        Debug.Log("Oda yüklendi: " + index);
    }
}