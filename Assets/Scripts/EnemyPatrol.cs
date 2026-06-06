using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    [Header("Movement")]
    public float chaseSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        base.Update();

        if (playerDetected)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * chaseSpeed;

            // Oyuncuya dön
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}