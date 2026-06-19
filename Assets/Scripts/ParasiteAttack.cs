using UnityEngine;

public class ParasiteAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackRadius = 1.5f;
    public float attackDamage = 25f;
    public float attackCooldown = 0.5f;

    private float lastAttackTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            TryAttack();
        }
    }

    public void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;
        lastAttackTime = Time.time;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue;

            HealthSystem health = hit.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
                Debug.Log(hit.gameObject.name + " hasar aldı!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}