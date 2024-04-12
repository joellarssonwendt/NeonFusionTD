using UnityEngine;

public class FireNormalTurret : MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject dotProjectilePrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;
    [SerializeField] private GameObject TemporaryTurretSprite;

    [Header("Stats")]
    [SerializeField] private TurretStats turretStats;

    private Transform target;
    private float timeUntilFire;
    private bool useFiringPoint1 = true;

    private void Update()
    {
        if (target == null)
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
        // Determine which firing point to use based on the flag
        Transform currentFiringPoint = useFiringPoint1 ? firingPoint1 : firingPoint2;

        // Instantiate a dot projectile and set its target
        GameObject projectileObject = Instantiate(dotProjectilePrefab, currentFiringPoint.position, Quaternion.identity);
        DotProjectile dotProjectile = projectileObject.GetComponent<DotProjectile>();

        // Set the damage value of the dot projectile from the Scriptable Object
        dotProjectile.SetDamage(turretStats.projectileDamage);
        dotProjectile.SetDotDamage(turretStats.dotAmount); // Set dot damage
        dotProjectile.SetDotDuration(turretStats.dotDuration); // Set dot duration
        dotProjectile.SetTarget(target);

        // Toggle the flag for the next shot
        useFiringPoint1 = !useFiringPoint1;
    }

    private void FindTarget()
    {   // Raycast in a circle around the turret's position to find enemies within targeting range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretStats.targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0) // If enemies are found within range, set the first one as target
        {
            target = hits[0].transform;
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

    private void OnDrawGizmosSelected()
    {   // Draws a circle in the scene view to visualize the turret's targeting range
        //Handles.color = Color.green;
        //Handles.DrawWireDisc(transform.position, transform.forward, turretStats.targetingRange);
    }

    private void SpawnTemporaryTowerSprite()
    {
        Instantiate(TemporaryTurretSprite, transform.position, Quaternion.identity);
    }

    private void MoveTemporaryTowerSprite()
    {

    }

    private void TouchPosition()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
