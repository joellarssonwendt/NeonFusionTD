using UnityEngine;

public class SuperFireTurret : MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject dotProjectilePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject TemporaryTurretSprite;
    [SerializeField] private ParticleSystem flamethrowerParticle;
    BuildManager buildManager;
    EnemySpawner enemySpawner;
    private GameObject currentTurretOnPointer;

    [Header("Stats")]
    [SerializeField] private TurretStats turretStats;

    private Transform target;
    private float timeUntilFire = 0f;

    private void Start()
    {
        enemySpawner = EnemySpawner.instance;
        buildManager = BuildManager.instance;
    }
    private void Update()
    {
        if (target == null || target.GetComponent<Enemy>().isDead)
        {
            StopFlamethrower();
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) // If the target is out of range, reset target to null
        {
            StopFlamethrower();
            target = null;
        }
        else
        {
            if (!flamethrowerParticle.isPlaying)
            {
                flamethrowerParticle.Play();
            }

            float projectileShootInterval = 1f / turretStats.projectilesPerSecond;
            if (Time.time >= timeUntilFire)
            {
                Shoot();
                timeUntilFire = Time.time + projectileShootInterval;
            }
        }
    }

    private void Shoot()
    {
        float coneAngle = 70;

        float flamethrowerRadius = CalculateFlamethrowerRadius();

        // Detect all enemies within the flamethrower's area of effect
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(firingPoint.position, flamethrowerRadius, enemyMask);
        Vector2 flamethrowerDirection = (firingPoint.position - transform.position).normalized;

        foreach (var enemy in enemiesInRange)
        {
            // Calculate the vector from the flamethrower to the enemy
            Vector2 toEnemy = (enemy.transform.position - firingPoint.position).normalized;

            // Calculate the angle between the flamethrower's direction and the vector to the enemy
            float angle = Vector2.Angle(flamethrowerDirection, toEnemy);

            // Check if the angle is within the cone's angle
            if (angle <= coneAngle / 2) 
            {
                GameObject projectileObject = Instantiate(dotProjectilePrefab, firingPoint.position, Quaternion.identity);
                DotProjectile dotProjectile = projectileObject.GetComponent<DotProjectile>();

                dotProjectile.SetDamage(turretStats.projectileDamage);
                dotProjectile.SetDotDamage(turretStats.dotAmount); 
                dotProjectile.SetDotDuration(turretStats.dotDuration); 
                dotProjectile.SetTarget(enemy.transform);
            }
        }
    }

    private void StopFlamethrower()
    {
        flamethrowerParticle.Stop();
        flamethrowerParticle.Clear(); // Clear existing particles
    }

    private void FindTarget()
    {
        // Raycast in a circle around the turret's position to find enemies within targeting range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretStats.targetingRange, (Vector2)transform.position, 0f, enemyMask);

        foreach (var hit in hits)
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            // Check if the enemy is not dead
            if (enemy != null && !enemy.isDead)
            {
                target = hit.transform;
                break;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= turretStats.targetingRange;
    }

    private void RotateTowardsTarget()
    {   // Calculate angle between turret and target, and rotate turret towards target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, turretStats.rotationSpeed * Time.deltaTime);
    }

    private float CalculateFlamethrowerRadius()
    {
        var main = flamethrowerParticle.main;
        return main.startSize.constant * 3.3f; 
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
         buildManager.selectBuiltTurret();
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
