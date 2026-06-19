using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private RoomManager roomManager;

    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Room room = GetComponentInParent<Room>();
        if (room != null && room.isCleared)
        {
            roomManager.NextRoom();
            Debug.Log("Kapıdan geçildi!");
        }
    }
}