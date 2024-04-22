using System.Collections.Generic;
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
        PolygonCollider2D polygonCollider = turretRotationPoint.GetComponent<PolygonCollider2D>();

        // Convert the polygon collider's points to world space
        Vector2[] worldPoints = new Vector2[polygonCollider.points.Length];
        for (int i = 0; i < polygonCollider.points.Length; i++)
        {
            worldPoints[i] = turretRotationPoint.TransformPoint(polygonCollider.points[i]);
        }

        // Find all enemies in range
        Collider2D[] allEnemies = Physics2D.OverlapCircleAll(transform.position, turretStats.targetingRange, enemyMask);

        // Filter enemies that are within the polygon collider's area
        List<Collider2D> enemiesInArea= new List<Collider2D>();
        foreach (var enemy in allEnemies)
        {
            if (IsPointInPolygon(enemy.transform.position, worldPoints))
            {
                enemiesInArea.Add(enemy);
            }
        }

        //Debug.Log("Enemies in collider area: " + enemiesInArea.Count);

        foreach (var enemy in enemiesInArea)
        {
            GameObject projectileObject = Instantiate(dotProjectilePrefab, firingPoint.position, Quaternion.identity);
            DotProjectile dotProjectile = projectileObject.GetComponent<DotProjectile>();

            dotProjectile.SetDamage(turretStats.projectileDamage);
            dotProjectile.SetDotDamage(turretStats.dotAmount);
            dotProjectile.SetDotDuration(turretStats.dotDuration);
            dotProjectile.SetTarget(enemy.transform);
        }
    }
    
    //Checks if given point is inside the polygon 
    private bool IsPointInPolygon(Vector2 point, Vector2[] polygonPoints)
    {
        bool isInside = false;
        int j = polygonPoints.Length - 1;
        for (int i = 0; i < polygonPoints.Length; i++)
        {
            if ((polygonPoints[i].y < point.y && polygonPoints[j].y >= point.y || polygonPoints[j].y < point.y && polygonPoints[i].y >= point.y) &&
                (polygonPoints[i].x + (point.y - polygonPoints[i].y) / (polygonPoints[j].y - polygonPoints[i].y) * (polygonPoints[j].x - polygonPoints[i].x) < point.x))
            {
                isInside = !isInside;
            }
            j = i;
        }
        return isInside;
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
