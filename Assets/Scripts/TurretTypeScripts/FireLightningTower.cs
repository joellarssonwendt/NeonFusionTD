using UnityEngine;

public class FireLightningTower : MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject dotProjectilePrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;
    [SerializeField] private GameObject TemporaryTurretSprite;
    EnemySpawner enemySpawner;
    BuildManager buildManager;
    AudioManager audioManager;
    private GameObject currentTurretOnPointer;
    
    [Header("Stats")]
    [SerializeField] private TurretStats turretStats;

    private Transform target;
    private float timeUntilFire;
    private bool useFiringPoint1 = true;

    private void Start()
    {
        enemySpawner = EnemySpawner.instance;
        buildManager = BuildManager.instance;
        audioManager = AudioManager.instance;
    }
    private void Update()
    {
        if (target == null || target.GetComponent<Enemy>().isDead)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) // If the target is out of range, reset target to null
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime; // Increment timeUntilFire and shoot if it's time to fire

            if (timeUntilFire >= 1f / turretStats.projectilesPerSecond)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() // Instantiate a projectile and set its target
    {
        audioManager.GetComponent<AudioManager>().PlaySoundEffect("FireShockAttack");

        // Determine which firing point to use based on the flag
        Transform currentFiringPoint = useFiringPoint1 ? firingPoint1 : firingPoint2;

        // Instantiate a dot projectile and set its target
        GameObject projectileObject = Instantiate(dotProjectilePrefab, currentFiringPoint.position, Quaternion.identity);
        DotProjectile dotProjectileScript = projectileObject.GetComponent<DotProjectile>();

        dotProjectileScript.SetDamage(turretStats.projectileDamage);
        dotProjectileScript.SetDotDamage(turretStats.dotDamagePerSecond); 
        dotProjectileScript.SetDotDuration(turretStats.dotDuration);
        dotProjectileScript.SetMaxChains(turretStats.maxChains);
        dotProjectileScript.SetChainRange(turretStats.chainRange);
        dotProjectileScript.SetEnemyMask(enemyMask);
        dotProjectileScript.SetTarget(target);

        // Toggle the flag for the next shot
        useFiringPoint1 = !useFiringPoint1;
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
        buildManager.ActivateTemporaryTurretSprite();
        buildManager.tileObject.SetTurretToNull();
    }

    private void OnMouseUp()
    {
        if (buildManager.tileObject.GetTurret() != null)
        {
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
