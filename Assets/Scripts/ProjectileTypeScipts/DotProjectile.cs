using UnityEngine;
using System.Collections;

public class DotProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Stats")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] public float dotDuration; 
    [SerializeField] private float dotDamage;
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

    public void SetDotDamage(float damage)
    {
        dotDamage = damage; 
    }

    public void SetDotDuration(float duration)
    {
        dotDuration = duration; 
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
        DrawLightningFireEffect();
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
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   // If the projectile collides with an object, deal damage to its health and destroy the projectile
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Apply initial damage
            enemy.TakeDamage(projectileDamage);

            // Apply DoT effect
            StartCoroutine(ApplyDotEffect(enemy));
        }

        Destroy(gameObject);
        ChainDamage();
    }

    private IEnumerator ApplyDotEffect(Enemy enemy)
    {
        // Add the dotDamage to the total DoT effect for this dotProjectile
        enemy.TakeDotDamage(dotDamage, this);

        //Debug.Log("DoT effect started on enemy: " + enemy.name);

        float timer = 0f;
        float damageInterval = 0.5f;

        while (timer < dotDuration)
        {
            yield return null;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(damageInterval);
            timer += damageInterval;

            enemy.TakeDotDamage(dotDamage, this);
        }

        // Remove the dotDamage from the total DoT effect when DoT effect ends
        dotDamage = 0;

        //Debug.Log("DoT effect ended on enemy: " + enemy.name);
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
                enemyHealth.TakeDotDamage(dotDamage, this);
                chainsMade++;
            }
        }
        // Debug log the number of chains made
        // Debug.Log("Chained to " + chainsMade + " additional enemies.");
    }

    private void DrawLightningFireEffect()
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


