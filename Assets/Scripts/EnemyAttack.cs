using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackRadius = 1f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private float lastAttackTime = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Possessed enemy'e saldır
        EnemyPatrol[] enemies = FindObjectsOfType<EnemyPatrol>();
        foreach (var enemy in enemies)
        {
            if (enemy.isPossessed)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist <= attackRadius && Time.time >= lastAttackTime + attackCooldown)
                {
                    enemy.GetComponent<HealthSystem>().TakeDamage(attackDamage);
                    lastAttackTime = Time.time;
                    Debug.Log("Possessed enemy hasar aldı!");
                    return;
                }
            }
        }

        // Normal Player'a saldır
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRadius && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Player hasar aldı! Can: " + playerHealth.currentHealth);
        }
    }
}