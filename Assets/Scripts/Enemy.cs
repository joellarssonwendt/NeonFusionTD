using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameObject fireIconPrefab;
    [SerializeField] private GameObject iceIconPrefab;
    [SerializeField] private int bossNumber = 0;
    [SerializeField] private bool obsidianResistant = false;
    [SerializeField] private Color fireColor1, fireColor2;

    private AudioManager audioManager;
    private EnemySpawner enemySpawner;
    private SpriteRenderer spriteRenderer;
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
    private Dictionary<ChillEffect, GameObject> iceIcons = new Dictionary<ChillEffect, GameObject>();
    private bool isMovingBackwards = false;
    private float obsidianEffectDuration = 3f;
    private bool isAffectedByObsidian = false;
    private bool bossActive = false;
    

    private void Start()
    {
        audioManager = AudioManager.instance;
        enemySpawner = EnemySpawner.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (bossNumber != 0 && !bossActive)
        {
            RunBossBehaviour();
        }
    }

    private void Move()
    {
        if (isMovingBackwards)
        {
            // If moving backwards, check if the enemy has reached the previous pathing node
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                // If the enemy has reached the previous pathing node, look back another index in the array of pathing nodes
                if (pathIndex > 0)
                {
                    pathIndex--;
                    target = LevelManager.main.pathingNodes[pathIndex];
                }
                else
                {
                    // If the enemy has reached the spawn point, stop moving backwards
                    if (Vector2.Distance(transform.position, LevelManager.main.spawnPoint.position) <= 0.1f)
                    {
                        isMovingBackwards = false;
                        // Since the enemy is already at the spawn point, reset pathIndex to 0 and set the target to the first node
                        pathIndex = 0;
                        target = LevelManager.main.pathingNodes[pathIndex];
                    }
                    else
                    {
                        // If the enemy has not reached the spawn point, set the target to the spawn point
                        target = LevelManager.main.spawnPoint;
                    }
                }
            }
        }
        else
        {
            // Existing logic for moving forwards...
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

        Vector2 direction = (target.position - transform.position).normalized;
        if (rb.bodyType == RigidbodyType2D.Kinematic) rb.velocity = direction * chilledMoveSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (bossNumber != 0) enemySpawner.bossHealthSlider.value = currentHealth;

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
        float iconOffset = 0.25f;
        GameObject fireIcon = Instantiate(fireIconPrefab, transform.position + Vector3.up * 1.0f + Vector3.right * iconOffset, Quaternion.identity, transform);
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
            if (bossNumber != 0) enemySpawner.bossHealthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            //Debug.Log($"Dot effect dealt {accumulatedDotDamage} damage");

            //Debug.Log("Total active burn effects: " + dotEffects.Count + ", Total accumulated Dot effect damage: " + accumulatedDotDamage);

            accumulatedDotDamage = 0f;

            // Update the last damage application time
            lastDamageApplicationTime = currentTime;
        }

    }

    public void ApplyChillEffect(float chillAmount, float duration, string sourceOfChill)
    {
        ChillEffect newChillEffect = new ChillEffect(chillAmount, duration, sourceOfChill);
        chillEffects.Add(newChillEffect);

        if (iceIcons.Count == 0)
        {
            CreateIceIcon(newChillEffect);
        }
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
                float appliedChillReduction = effect.sourceOfChill == "ArcticTower" ? 0.6f : 0.7f;
                float appliedChillAmount = Mathf.Min(effect.amount, appliedChillReduction);
                totalChillEffect += appliedChillAmount;

                // Update maxChillAmount if the current effect's max chill amount is greater
                maxChillEffectAmount = Mathf.Max(maxChillEffectAmount, appliedChillReduction);
            }
        }

        // Check if there are no more active chill effects
        if (chillEffects.Count == 0)
        {
            // Destroy all ice icons associated with the enemy
            foreach (var iceIcon in iceIcons.Values)
            {
                Destroy(iceIcon);
            }
            iceIcons.Clear();   // Clear the dictionary to reflect that there are no active chill effects

        }

        // Apply the chill effect based on the total chill amount, capping at maxChillAmount
        chilledMoveSpeed = Mathf.Max(originalMoveSpeed * (1 - totalChillEffect), originalMoveSpeed * maxChillEffectAmount);

        //Debug.Log("Total active chill effects: " + chillEffects.Count + ", Current movement speed reduction: " + ((1 - (chilledMoveSpeed / originalMoveSpeed)) * 100) + "%");
    }
    public void CreateIceIcon(ChillEffect chillEffect)
    {
        float iconOffset = 0.25f;
        GameObject iceIcon = Instantiate(iceIconPrefab, transform.position + Vector3.up * 1.0f + Vector3.left * iconOffset, Quaternion.identity, transform);
        iceIcons.Add(chillEffect, iceIcon); 
    }

    public void HitByObsidian()
    {
        if (obsidianResistant) return;

        if (!isAffectedByObsidian)
        {
            isAffectedByObsidian = true;

            isMovingBackwards = true;
            StartCoroutine(ObsidianEffectCoroutine());

            // Adjust the target to the previous pathing node
            if (pathIndex > 0)
            {
                pathIndex--;
                target = LevelManager.main.pathingNodes[pathIndex];
            }
        }
    }

    private IEnumerator ObsidianEffectCoroutine()
    {
        yield return new WaitForSeconds(obsidianEffectDuration);

        // Reset the enemy's movement logic
        isMovingBackwards = false;
        target = LevelManager.main.pathingNodes[pathIndex];
        isAffectedByObsidian = false;
        //Debug.Log("New target after backward movement duration expired: " + target.name);
    }

    private void Die()
    {
        isDead = true; // Set the flag to true to indicate that the enemy has died
        audioManager.Play("EnemyDeath");
        EnemySpawner.onEnemyDestroy.Invoke();
        animator.Play("Ghost_Death");

        foreach (var fireIcon in fireIcons.Values)
        {
            Destroy(fireIcon);
        }
        foreach (var iceIcon in iceIcons.Values)
        {
            Destroy(iceIcon);
        }

        if (bossNumber != 0)
        {
            bossActive = false;
            bossNumber = 0;
            audioManager.Play("BossDeath");
            enemySpawner.bossHealthObject.SetActive(false);
        }
        Destroy(gameObject, 1f);
    }

    private void RunBossBehaviour()
    {
        bossActive = true;

        audioManager.Play("BossSpawn");

        enemySpawner.bossHealthSlider.maxValue = currentHealth;
        enemySpawner.bossHealthSlider.value = currentHealth;
        enemySpawner.bossHealthObject.SetActive(true);

        // Spela "Boss Fight Startar" ljudeffekt

        if (bossNumber == 1)
        {
            Debug.Log("kör Boss 1 beteende");
            StartCoroutine(Boss1());
        }

        if (bossNumber == 2)
        {
            Debug.Log("kör Boss 2 beteende");
        }
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

    public class ChillEffect
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


    // Coroutines för eld och is-effekter
   /* private IEnumerator FireEffect()
    {
        Color orginialColor = spriteRenderer.color;
        float elapsedTime = 0f;  
        while(accumulatedDotDamage < elapsedTime)
        {
            float interval = 0.25f;

            spriteRenderer.color = spriteRenderer.color;
            yield return new WaitForSeconds(interval);

            spriteRenderer.color = fireColor1;
            yield return new WaitForSeconds(interval);

            spriteRenderer.color = fireColor2;
            yield return new WaitForSeconds(interval);

            if (accumulatedDotDamage <= 0)
            {
                spriteRenderer.color = orginialColor; // Restore the original color
                
            }
            
        }
    }*/
   

    // Boss beteenden
    private IEnumerator Boss1()
    {
        Color originalColor = spriteRenderer.color;
        float maxHealth = currentHealth;

        while (currentHealth > 0)
        {
            if (currentHealth < maxHealth)
            {
                yield return new WaitForSeconds(3f);
                spriteRenderer.color = Color.white;
                currentHealth += 10;
                if (currentHealth > maxHealth) currentHealth = maxHealth;
                enemySpawner.bossHealthSlider.value = currentHealth;
                Debug.Log(currentHealth);
                rb.bodyType = RigidbodyType2D.Static;
                yield return new WaitForSeconds(1f);
                rb.bodyType = RigidbodyType2D.Kinematic;
                spriteRenderer.color = originalColor;
            }
            
            yield return null;
        }
    }
}
