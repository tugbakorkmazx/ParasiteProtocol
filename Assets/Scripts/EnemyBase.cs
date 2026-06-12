using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRadius = 5f;
    protected Transform player;
    protected bool playerDetected = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

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