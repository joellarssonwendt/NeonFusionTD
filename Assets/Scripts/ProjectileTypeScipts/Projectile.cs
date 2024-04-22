using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 10f;
    private float projectileDamage = 0;
    private int maxChains;
    private float chainRange;
    private LayerMask enemyMask;
    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }

    public void SetMaxChains(int maxChains)
    {
        this.maxChains = maxChains;
    }

    public void SetChainRange(float chainRange)
    {
        this.chainRange = chainRange;
    }

    public void SetEnemyMask(LayerMask mask)
    {
        enemyMask = mask;
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterTime(3f));
        DrawLightningEffect();
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized; // Calculate the direction towards the target and normalize it

        rb.velocity = direction * projectileSpeed; // Set the velocity of the Rigidbody to move the projectile towards the target
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Destroy the projectile
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the projectile collides with an object, deal damage to its health and destroy the projectile
        Enemy health = other.gameObject.GetComponent<Enemy>();
        if (health != null)
        {
            health.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
        ChainDamage();
    }

    private void ChainDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, chainRange, enemyMask);

        int chainsMade = 0;

        foreach (var enemy in enemies)
        {
            // Skip the main target
            if (enemy.transform == target) continue;

            if (chainsMade >= maxChains) break; // Stop chaining after reaching the maximum number of chains

            Enemy enemyHealth = enemy.GetComponent<Enemy>();
            if (enemyHealth != null && !enemyHealth.isDead)
            {
                enemyHealth.TakeDamage(projectileDamage);
                chainsMade++; 
            }
        }
        // Debug log the number of chains made
        Debug.Log("Chained to " + chainsMade + " additional enemies.");
    }

    private void DrawLightningEffect()
    {
        if (maxChains <= 0) return;
        
        // Reset the LineRenderer
        lineRenderer.positionCount = 0;

        // Add the projectile's position as the first point
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(0, transform.position);

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(1, target.position);

        int chainedTargetsCount = 0;

        // Add each chained enemy's position as subsequent points
        foreach (var enemy in Physics2D.OverlapCircleAll(transform.position, chainRange, enemyMask))
        {
            if (enemy.transform == target) continue;

            Enemy enemyHealth = enemy.GetComponent<Enemy>();
            if (enemyHealth != null && enemyHealth.isDead) continue;

            if (chainedTargetsCount >= maxChains) break;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, enemy.transform.position);

            chainedTargetsCount++;
        }
    }
}
