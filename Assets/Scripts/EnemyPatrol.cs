using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    [Header("Movement")]
    public float chaseSpeed = 3f;
    public float patrolSpeed = 1.5f;

    [HideInInspector] public bool isPossessed = false;

    private Rigidbody2D rb;
    private EnemyPerception perception;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        perception = GetComponent<EnemyPerception>();
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    new void Update()
    {
        if (isPossessed)
        {
            Vector2 input;
            ParasiteMovement pm = GameObject.FindWithTag("Player").GetComponent<ParasiteMovement>();
            if (pm != null && pm.joystick != null && pm.joystick.Direction.magnitude > 0.1f)
                input = pm.joystick.Direction;
            else
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            rb.velocity = input * chaseSpeed;
            FlipSprite(input.x);
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
            FlipSprite(direction.x);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void FlipSprite(float xDir)
    {
        if (sr == null) return;
        if (xDir > 0.1f) sr.flipX = false;
        else if (xDir < -0.1f) sr.flipX = true;
    }
}