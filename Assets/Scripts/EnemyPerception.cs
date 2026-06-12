using UnityEngine;

public class EnemyPerception : MonoBehaviour
{
    [Header("Görüş")]
    public float viewRadius = 6f;
    [Range(0, 360)] public float viewAngle = 90f;
    public LayerMask playerLayer;

    [Header("Ses")]
    public float hearingRadius = 3f;

    [HideInInspector] public bool playerVisible = false;
    [HideInInspector] public bool playerHeard = false;

    private Transform player;
    private ParasiteController parasite;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        parasite = FindObjectOfType<ParasiteController>();
    }

    void Update()
    {
        if (player == null) return;

        CheckVision();
        CheckHearing();
    }

    void CheckVision()
{
    playerVisible = false;

    // Possessed enemy varsa onu ara, yoksa normal player
    Transform target = player;
    if (parasite != null && parasite.IsHostValid())
        target = parasite.GetHost().transform;

    if (target == null) return;

    Vector2 dirToTarget = (target.position - transform.position).normalized;
    float distToTarget = Vector2.Distance(transform.position, target.position);

    if (distToTarget > viewRadius) return;

    float angle = Vector2.Angle(transform.up, dirToTarget);
    if (angle < viewAngle / 2f)
    {
        // Possessed enemy kendi türündeyse görme
        if (parasite != null && parasite.IsHostValid()) return;
        playerVisible = true;
    }
}

    void CheckHearing()
{
    playerHeard = false;

    if (parasite == null) return;
    if (parasite.IsHostValid()) return;

    float distToPlayer = Vector2.Distance(transform.position, player.position);
    if (distToPlayer <= hearingRadius)
    {
        ParasiteMovement pm = player.GetComponent<ParasiteMovement>();
        if (pm != null && !pm.isSneaking && pm.GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f)
        {
            playerHeard = true;
        }
    }
}

    void OnDrawGizmosSelected()
    {
        // Görüş konisi
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftDir = Quaternion.Euler(0, 0, viewAngle / 2f) * transform.up;
        Vector3 rightDir = Quaternion.Euler(0, 0, -viewAngle / 2f) * transform.up;
        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewRadius);

        // Ses algısı
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, hearingRadius);
    }
}