using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStats enemyStats;

    private Transform target;
    private int pathIndex = 0;
    private float currentHealth;
    private Dictionary<DotProjectile, float> dotEffects = new Dictionary<DotProjectile, float>(); // Dictionary to store DoT effects

    private void Start()
    {
        target = LevelManager.main.pathingNodes[pathIndex];
        currentHealth = enemyStats.maxHealth;
    }

    private void Update()
    {
        Move();
        ApplyDotEffects();
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
            Die();
        }
    }

    public void TakeDotDamage(float dotDamage, DotProjectile dotProjectile)
    {
        // Add the dotDamage to the total DoT effect for this dotProjectile
        if (dotEffects.ContainsKey(dotProjectile))
        {
            dotEffects[dotProjectile] += dotDamage;
        }
        else
        {
            dotEffects.Add(dotProjectile, dotDamage);
        }

        // Log the total DoT effect for this dotProjectile
        Debug.Log($"DoT effect added to: {dotEffects[dotProjectile]}");
    }


    private void ApplyDotEffects()
    {
        foreach (var dotEffect in dotEffects)
        {
            float damageApplied = dotEffect.Value * Time.deltaTime;
            currentHealth -= damageApplied;

            // Log the damage applied by the DoT effect
            Debug.Log($"DoT effect dealt {damageApplied} damage");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        EnemySpawner.onEnemyDestroy.Invoke();
        animator.Play("Ghost_Death");
        Destroy(gameObject, 1f);
    }
}
