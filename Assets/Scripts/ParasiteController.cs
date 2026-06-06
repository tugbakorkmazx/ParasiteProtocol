using UnityEngine;

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

            currentHost.enabled = false;
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

    currentHost.enabled = true;
    currentHost = null;
    isPossessing = false;

    GetComponentInChildren<SpriteRenderer>().enabled = true;
    GetComponent<Rigidbody2D>().simulated = true;

    Debug.Log("Bedenden çıkıldı!");
}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, possessRadius);
    }
}