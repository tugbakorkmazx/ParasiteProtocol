using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    [Header("Movement")]
    public float chaseSpeed = 3f;
    public float patrolSpeed = 1.5f;

    [HideInInspector] public bool isPossessed = false;

    private Rigidbody2D rb;
    private EnemyPerception perception;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    perception = GetComponent<EnemyPerception>();
    player = GameObject.FindWithTag("Player").transform;
}

    new void Update()
    {
        if (isPossessed)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            rb.velocity = input * chaseSpeed;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mousePos - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            return;
        }

        base.Update();

        if (player == null) return;

        bool detected = perception != null
            ? (perception.playerVisible || perception.playerHeard)
            : playerDetected;

        if (detected)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * chaseSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}