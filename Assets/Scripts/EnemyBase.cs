using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRadius = 5f;
    public Transform player;

    protected bool playerDetected = false;

    protected void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        playerDetected = distance <= detectionRadius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}