using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStats enemyStats;

    private Transform target;
    private int pathIndex = 0;
    private float currentHealth;

    private void Start()
    {
        target = LevelManager.main.pathingNodes[pathIndex];
        currentHealth = enemyStats.maxHealth;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
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

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * enemyStats.moveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            animator.Play("Ghost_Death");
            Invoke("EnemyDeath", 1f);
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
    }
}