using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currentWaypointIndex;
    private float speed;
    public float patrolSpeed = 2f;
    public Transform[] waypoints;

    void Start()
    {
        speed = patrolSpeed;
    }

    void Update()
    {

        enemyPatrol();

    }

    void enemyPatrol()
    {
        if (waypoints.Length == 0) return;
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = (targetWaypoint.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "base")
        {
            Debug.Log("Enemy reached base!");
            Destroy(gameObject);
        }
    }

}
