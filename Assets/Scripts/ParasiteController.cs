using UnityEngine;
using System.Collections;

public class ParasiteController : MonoBehaviour
{
    public float possessRadius = 1.5f;
    private EnemyPatrol currentHost = null;
    private bool isPossessing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPossessing)
                TryPossess();
            else
                LeavePossess();
        }
    }

    void TryPossess()
{
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, possessRadius);
    foreach (var hit in hits)
    {
        EnemyPatrol enemy = hit.GetComponent<EnemyPatrol>();
        if (enemy != null)
        {
            currentHost = enemy;
            isPossessing = true;

            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;

            currentHost.isPossessed = true;
            transform.SetParent(currentHost.transform);
            transform.localPosition = Vector3.zero;

            Debug.Log("Bedene girildi!");
            break;
        }
    }
}

void LeavePossess()
{
    if (currentHost == null) return;

    transform.SetParent(null);
    transform.position = currentHost.transform.position + Vector3.up;

    currentHost.isPossessed = false;
    currentHost = null;
    isPossessing = false;

    GetComponentInChildren<SpriteRenderer>().enabled = true;
    GetComponent<Rigidbody2D>().simulated = true;

    // Parasite halindeyken — 1 can
    GetComponent<HealthSystem>().currentHealth = 1f;

    Debug.Log("Bedenden çıkıldı!");

    StartCoroutine(InvincibilityFrames());
}
public HealthSystem GetCurrentHealth()
{
    if (isPossessing && currentHost != null)
        return currentHost.GetComponent<HealthSystem>();
    else
        return GetComponent<HealthSystem>();
}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, possessRadius);
    }
        public void ForceLeavePossess()
    {
        LeavePossess();
    }

    IEnumerator InvincibilityFrames()
{
    HealthSystem hs = GetComponent<HealthSystem>();
    hs.isInvincible = true;
    // İsteğe bağlı: yanıp sönsün
    SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
    for (int i = 0; i < 6; i++)
    {
        sr.enabled = !sr.enabled;
        yield return new WaitForSeconds(0.15f);
    }
    sr.enabled = true;
    hs.isInvincible = false;
}

    public bool IsHostValid()
    {
        return isPossessing && currentHost != null;
    }

    public EnemyPatrol GetHost()
    {
        return currentHost;
    }

}