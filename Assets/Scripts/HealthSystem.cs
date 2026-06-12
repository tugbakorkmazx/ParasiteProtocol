using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [HideInInspector] public bool isInvincible = false;

    void Start()
    {
        if (gameObject.CompareTag("Player"))
            currentHealth = 1f;
        else
            currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameOverUI>().ShowGameOver();
        }
        else
        {
            EnemyPatrol ep = GetComponent<EnemyPatrol>();
            if (ep != null && ep.isPossessed)
            {
                FindObjectOfType<ParasiteController>().ForceLeavePossess();
            }
            Destroy(gameObject);
        }
    }
}