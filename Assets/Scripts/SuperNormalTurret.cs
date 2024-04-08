using UnityEngine;
using UnityEditor;

public class SuperNormalTurret : MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")] // Header to group serialized fields in the inspector
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float pps = 2f; // Projectiles Per Second 

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if(target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange()) // If the target is out of range, reset target to null
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime; // Increment timeUntilFire and shoot if it's time to fire

            if (timeUntilFire >= 1f / pps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() // Instantiate a projectile and set its target
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        projectileScript.SetTarget(target);
    }

    private void FindTarget()
    {
        // Raycast in a circle around the turret's position to find enemies within targeting range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) // If enemies are found within range, set the first one as target
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Calculate angle between turret and target, and rotate turret towards target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Draws a circle in the scene view to visualize the turret's targeting range
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
