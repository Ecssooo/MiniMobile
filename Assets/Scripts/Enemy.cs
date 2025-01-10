using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP = 50;
    public int damageToBase = 10;
    public float patrolSpeed = 2f;
    public Transform[] waypoints;
    public Transform healthBar;
    public Vector3 healthBarOffset = new Vector3(0, 1, 0);

    int currentWaypointIndex;

    void Start()
    {
        float ratio = (float)currentHP / maxHP;
        if (healthBar != null)
        {
            healthBar.localScale = new Vector3(Mathf.Clamp01(ratio), healthBar.localScale.y, healthBar.localScale.z);
        }
    }

    void Update()
    {
        if (waypoints != null && currentWaypointIndex < waypoints.Length)
        {
            Vector2 dir = (waypoints[currentWaypointIndex].position - transform.position).normalized;
            transform.position += (Vector3)(dir * patrolSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        if (healthBar != null)
        {
            healthBar.position = transform.position + healthBarOffset;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        float ratio = Mathf.Clamp01((float)currentHP / maxHP);
        if (healthBar != null)
        {
            healthBar.localScale = new Vector3(ratio, healthBar.localScale.y, healthBar.localScale.z);
        }
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BaseHealth b)) //if (collision.CompareTag("base"))
        {
            b.TakeDamage(damageToBase);
            Destroy(gameObject);
            
            // BaseHealth b = collision.GetComponent<BaseHealth>();
            // if (b != null) {}
        }
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentWaypointIndex = 0;
    }
}
