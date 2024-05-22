using UnityEngine;

public class ObsidianTower: MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject obsidianProjectilePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject tilePrefab;
    BuildManager buildManager;
    EnemySpawner enemySpawner;
    AudioManager audioManager;
    private GameObject currentTurretOnPointer;

    [Header("Stats")]
    [SerializeField] private TurretStats turretStats;


    private Transform target;
    private float timeUntilFire;
    public GameObject turretsTile;

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
        audioManager.PlaySoundEffect("ObsidianAttack");

        GameObject projectileObject = Instantiate(obsidianProjectilePrefab, firingPoint.position, Quaternion.identity);
        ObsidianProjectile obsidianProjectileScript = projectileObject.GetComponent<ObsidianProjectile>();
        obsidianProjectileScript.SetTarget(target);
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
    {
        // Calculate angle between turret and target, and rotate turret towards target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, turretStats.rotationSpeed * Time.deltaTime);
    }
}
