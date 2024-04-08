using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Cache
    private Rigidbody2D rb;

    // Variables
    [SerializeField] private float moveSpeed;

    private Transform target;
    private int pathIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = LevelManager.main.pathingNodes[pathIndex];
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.pathingNodes.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.pathingNodes[pathIndex];
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

}
