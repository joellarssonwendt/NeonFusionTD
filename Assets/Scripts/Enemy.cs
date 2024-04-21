using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameObject fireIconPrefab;
    [SerializeField] private GameObject iceIconPrefab;

    private Transform target;
    private int pathIndex = 0;
    private float currentHealth;
    public bool isDead = false;
    private float dotDamageTimer = 0f; // Timer to track the time since the last DoT damage application
    private float accumulatedDotDamage = 0f;
    private float originalMoveSpeed;
    private float chilledMoveSpeed;
    private float chillTimer;
    private Dictionary<DotProjectile, float> dotEffects = new Dictionary<DotProjectile, float>(); // Dictionary to store DoT effects
    private Dictionary<DotProjectile, GameObject> fireIcons = new Dictionary<DotProjectile, GameObject>();

    private void Start()
    {
        target = LevelManager.main.pathingNodes[pathIndex];
        currentHealth = enemyStats.maxHealth;

        originalMoveSpeed = enemyStats.moveSpeed;
        chilledMoveSpeed = originalMoveSpeed;
        chillTimer = 0f;
    }

    private void Update()
    {
        Move();
        ApplyDotEffects();
        CheckChillDuration();
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
        rb.velocity = direction * chilledMoveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead) // Check if the enemy is not already dead
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
            CreateFireIcon(dotProjectile);
        }

        // Log the total DoT effect for this dotProjectile
        //Debug.Log($"DoT effect added to: {dotEffects[dotProjectile]}");
    }

    public void CreateFireIcon(DotProjectile dotProjectile)
    {
        GameObject fireIcon = Instantiate(fireIconPrefab, transform.position + Vector3.up * 1.0f, Quaternion.identity, transform);
        fireIcons.Add(dotProjectile, fireIcon);
    }


    private void ApplyDotEffects()
    {
        if (isDead) return;

        dotDamageTimer += Time.deltaTime; // Increment the timer by the time passed since the last frame

        // Accumulate the total DoT damage
        foreach (var dotEffect in dotEffects)
        {
            accumulatedDotDamage += dotEffect.Value * Time.deltaTime;
        }

        if (dotDamageTimer >= 0.5f) 
        {
            // Apply the accumulated DoT damage
            currentHealth -= accumulatedDotDamage;

            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            // Log the damage applied by the DoT effect
            //Debug.Log($"DoT effect dealt {accumulatedDotDamage} damage");

            // Reset the accumulated DoT damage and the timer
            accumulatedDotDamage = 0f;
            dotDamageTimer = 0f;
        }
    }

    public void ApplyChillEffect(float chillAmount, float chillDuration)
    {
        Debug.Log("Applying chill effect to enemy: " + gameObject.name);
        chilledMoveSpeed = Mathf.Max(originalMoveSpeed * (1 - chillAmount), originalMoveSpeed * 0.3f); // Apply chill effect with 30% cap
        chillTimer = chillDuration;
        Debug.Log("Current chill amount: " + chillAmount + ", Current chill duration: " + chillDuration);
    }

    private void CheckChillDuration()
    {
        if (chillTimer > 0)
        {
            chillTimer -= Time.deltaTime;
            if (chillTimer <= 0)
            {
                chilledMoveSpeed = originalMoveSpeed;
            }
        }
    }


    private void Die()
    {
        isDead = true; // Set the flag to true to indicate that the enemy has died
        EnemySpawner.onEnemyDestroy.Invoke();
        animator.Play("Ghost_Death");

        foreach (var fireIcon in fireIcons.Values)
        {
            Destroy(fireIcon);
        }

        Destroy(gameObject, 1f);
    }
}
