using System;
using UnityEngine;

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
    private bool hasPlayedBuildupSound = false;

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

            float projectileShootInterval = 1f / turretStats.projectilesPerSecond;
            if (Time.time >= timeUntilFire)
            {
                Shoot(enemies);
                timeUntilFire = Time.time + projectileShootInterval;
            }

            if (!hasPlayedBuildupSound)
            {
                audioManager.PlaySoundEffect("TeslaTowerBuildup");
                hasPlayedBuildupSound = true;
            }
        }
        else
        {
            teslaParticleSystem.Stop();
            hasPlayedBuildupSound = false;
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

        float baseTotalDamage = turretStats.projectileDamage * Math.Min(validTargetsCount, 5);
        float adjustedDamage = validTargetsCount <= 5 ? turretStats.projectileDamage : baseTotalDamage / validTargetsCount;

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


    /* private void OnDrawGizmosSelected()
     {   // Draws a circle in the scene view to visualize the turret's targeting range
         //Handles.color = Color.green;
         //Handles.DrawWireDisc(transform.position, transform.forward, turretStats.targetingRange);
     }

     private void OnMouseDown()
     {
         currentTurretOnPointer = gameObject;
         buildManager.selectedTurret = currentTurretOnPointer;
         buildManager.ActivateTemporaryTurretSprite();
         buildManager.tileObject.SetTurretToNull();
     }

     private void OnMouseUp()
     {
         if (buildManager.tileObject.GetTurret() != null)
         {
             //här kan merge koden vara sen
             buildManager.deselectBuiltTurret();
             Debug.Log("deselect, Men kan köra merge också sen");
         }
         if (buildManager.tileObject.GetTurret() == null)
         {
             if (buildManager.isRaycastHittingTile() && !enemySpawner.activeRoundPlaying)
             {
                 //här flyttas turreten till tilen som musen är över
                 Debug.Log("flytta turret");
                 buildManager.selectedTurret.transform.position = buildManager.tileObject.transform.position;
                 buildManager.tileObject.SetTurretToNull();
                 buildManager.deselectBuiltTurret();
             }
             else
             {
                 //här deselectas turreten samt Temp sprites försvinner för att man missar rutan.
                 buildManager.deselectBuiltTurret();
                 Debug.Log("deselect");
             }
         }
     }
     public GameObject GetTurret()
     {
         return currentTurretOnPointer;
     }*/
}
