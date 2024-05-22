using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeslaTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject TemporaryTurretSprite;
    [SerializeField] private ParticleSystem teslaParticleSystem;

    BuildManager buildManager;
    EnemySpawner enemySpawner;
    AudioManager audioManager;
    private GameObject currentTurretOnPointer;

    [Header("Stats")]
    [SerializeField] private TurretStats turretStats;

    private float timeUntilFire = 0f;
    private bool soundIsPlaying = false;
    private float timeSinceLastTargetFoundOrKilled = 0f;
    private int enemiesForMaxTotalDamage = 5;

    private void Start()
    {
        enemySpawner = EnemySpawner.instance;
        buildManager = BuildManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Update()
    {
        float Range = turretStats.targetingRange;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(turretRotationPoint.position, Range, enemyMask);

        bool aliveEnemyInRange = false;
        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                aliveEnemyInRange = true;
                break;
            }
        }

        if (aliveEnemyInRange)
        {
            teslaParticleSystem.Play();
            timeSinceLastTargetFoundOrKilled = Time.time;

            float projectileShootInterval = 1f / turretStats.projectilesPerSecond;
            if (Time.time >= timeUntilFire)
            {
                Shoot(enemies);
                timeUntilFire = Time.time + projectileShootInterval;
            }

            if (!soundIsPlaying)
            {
                soundIsPlaying = true;
                audioManager.PlaySoundEffect("TeslaTower");
            }

        }
        else
        {
            teslaParticleSystem.Stop();
            if (timeSinceLastTargetFoundOrKilled >= 0.5f && !aliveEnemyInRange)
            {
                audioManager.Stop("TeslaTower");
                soundIsPlaying = false;
            }
            timeSinceLastTargetFoundOrKilled = Time.time;
        }
    }

    private void Shoot(Collider2D[] enemies)
    {
        int validTargetsCount = 0;
        audioManager.PlaySoundEffect("ShockAttack");

        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                validTargetsCount++;
            }
        }

        float baseTotalDamage = turretStats.projectileDamage * Math.Min(validTargetsCount, enemiesForMaxTotalDamage);
        float adjustedDamage = validTargetsCount <= enemiesForMaxTotalDamage ? turretStats.projectileDamage : baseTotalDamage / validTargetsCount;

        foreach (var enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null && !enemyComponent.isDead)
            {
                GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
                Projectile projectileScript = projectileObject.GetComponent<Projectile>();

                projectileScript.SetDamage(adjustedDamage);
                projectileScript.SetTarget(enemy.transform);

                projectileScript.SetMaxChains(turretStats.maxChains);
                projectileScript.SetChainRange(turretStats.chainRange);
                projectileScript.SetEnemyMask(enemyMask);
            }
        }
    }
}
