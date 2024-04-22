using UnityEngine;
using System.Collections.Generic;
using static Unity.VisualScripting.Member;

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
    private List<DotEffect> dotEffects = new List<DotEffect>();
    private float accumulatedDotDamage = 0f;
    private float lastDamageApplicationTime = 0f;
    private float damageApplicationInterval = 0.5f;
    private List<ChillEffect> chillEffects = new List<ChillEffect>();
    private float originalMoveSpeed;
    private float chilledMoveSpeed;
    private Dictionary<DotProjectile, GameObject> fireIcons = new Dictionary<DotProjectile, GameObject>();

    private void Start()
    {
        target = LevelManager.main.pathingNodes[pathIndex];
        currentHealth = enemyStats.maxHealth;

        originalMoveSpeed = enemyStats.moveSpeed;
        chilledMoveSpeed = originalMoveSpeed;
    }

    private void Update()
    {
        Move();
        ApplyDotEffects();
        CheckDoTDuration();
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
        if (isDead)
        {
            return;
        }

        dotEffects.Add(new DotEffect(dotDamage, dotProjectile.dotDuration));

        if (!fireIcons.ContainsKey(dotProjectile))
        {
            CreateFireIcon(dotProjectile);
        }

        //Debug.Log($"Dot effect added to: {dotEffects[dotEffects.Count - 1].damage}");
    }

    private void CheckDoTDuration()
    {
        for (int i = dotEffects.Count - 1; i >= 0; i--)
        {
            DotEffect dotEffect = dotEffects[i];
            dotEffect.duration -= Time.deltaTime;

            if (dotEffect.duration <= 0)
            {
                dotEffects.RemoveAt(i);  // Remove the DoT effect when its duration expires

            }
        }

        if (dotEffects.Count == 0)
        {
            // If there are no more dot effects, remove the fire icon associated with the enemy
            foreach (var fireIcon in fireIcons.Values)
            {
                Destroy(fireIcon);
            }
            fireIcons.Clear();  // Clear the dictionary to reflect that there are no active dot effects

        }
    }

    public void CreateFireIcon(DotProjectile dotProjectile)
    {
        GameObject fireIcon = Instantiate(fireIconPrefab, transform.position + Vector3.up * 1.0f, Quaternion.identity, transform);
        fireIcons.Add(dotProjectile, fireIcon);
    }

    private void ApplyDotEffects()
    {
        if (isDead) return;

        float currentTime = Time.time;
        if (currentTime - lastDamageApplicationTime >= damageApplicationInterval)
        {
            // Calculate the actual elapsed time since the last application
            float elapsedTime = currentTime - lastDamageApplicationTime;

            // Accumulate the total Dot damage based on the actual elapsed time
            foreach (var dotEffect in dotEffects)
            {
                accumulatedDotDamage += dotEffect.damage * elapsedTime;
            }

            // Apply the accumulated Dot damage
            currentHealth -= accumulatedDotDamage;

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            //Debug.Log($"Dot effect dealt {accumulatedDotDamage} damage");

            Debug.Log("Total active burn effects: " + dotEffects.Count + ", Total accumulated Dot effect damage: " + accumulatedDotDamage);

            // Reset the accumulated Dot damage
            accumulatedDotDamage = 0f;

            // Update the last damage application time
            lastDamageApplicationTime = currentTime;
        }

    }

    public void ApplyChillEffect(float chillAmount, float duration, string sourceOfChill)
    {
        // Add a new chill effect to the list
        chillEffects.Add(new ChillEffect(chillAmount, duration, sourceOfChill));

    }

    private void CheckChillDuration()
    {
        float totalChillEffect = 0f;
        float maxChillEffectAmount = 0f;

        for (int i = chillEffects.Count - 1; i >= 0; i--)
        {
            ChillEffect effect = chillEffects[i];
            effect.duration -= Time.deltaTime;
            if (effect.duration <= 0)
            {
                // Remove the chill effect when its duration expires
                chillEffects.RemoveAt(i);
            }
            else
            {
                // Accumulate the chill amount based on the source
                float appliedChillReduction = effect.sourceOfChill == "ArcticTower" ? 0.5f : 0.7f;
                float appliedChillAmount = Mathf.Min(effect.amount, appliedChillReduction);
                totalChillEffect += appliedChillAmount;

                // Update maxChillAmount if the current effect's max chill amount is greater
                maxChillEffectAmount = Mathf.Max(maxChillEffectAmount, appliedChillReduction);
            }
        }

        // Apply the chill effect based on the total chill amount, capping at maxChillAmount
        chilledMoveSpeed = Mathf.Max(originalMoveSpeed * (1 - totalChillEffect), originalMoveSpeed * maxChillEffectAmount);

        //Debug.Log("Total active chill effects: " + chillEffects.Count + ", Current movement speed reduction: " + ((1 - (chilledMoveSpeed / originalMoveSpeed)) * 100) + "%");
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

    private class DotEffect
    {
        public float damage;
        public float duration;

        public DotEffect(float damage, float duration)
        {
            this.damage = damage;
            this.duration = duration;
        }
    }

    private class ChillEffect
    {
        public float amount;
        public float duration;
        public string sourceOfChill;

        public ChillEffect(float amount, float duration, string sourceOfChill)
        {
            this.amount = amount;
            this.duration = duration;
            this.sourceOfChill = sourceOfChill;
        }
    }
}
