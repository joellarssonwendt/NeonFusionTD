using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class NormalTurret : MonoBehaviour
{
    [Header("References")] // Header to group serialized fields in the inspector
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret;
    [SerializeField] private GameObject tilePrefab;
    BuildManager buildManager;
    private Tile tile;

    [Header("Stats")] 
    [SerializeField] private TurretStats turretStats; 
    

    private Transform target;
    private float timeUntilFire;
    private GameObject currentTurretOnPointer;
    private GameObject tileWhenTowerWasClicked;

    private void Start()
    {
        buildManager = BuildManager.instance;
        tempNormalTurret = GameObject.FindWithTag("TemporaryNormalSprite");
        tempFireTurret = GameObject.FindWithTag("TemporaryFireSprite");
        tempIceTurret = GameObject.FindWithTag("TemporaryIceSprite");
        tempLightningTurret = GameObject.FindWithTag("TemporaryLightningSprite");
        tile = tilePrefab.GetComponent<Tile>();
        
    }
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
        GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();

        // Set the damage value of the projectile from the Scriptable Object
        projectileScript.SetDamage(turretStats.projectileDamage);

        projectileScript.SetTarget(target);
    }

    private void FindTarget()
    {
        // Raycast in a circle around the turret's position to find enemies within targeting range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretStats.targetingRange, (Vector2)transform.position, 0f, enemyMask);

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
    {
        // Calculate angle between turret and target, and rotate turret towards target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, turretStats.rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Draws a circle in the scene view to visualize the turret's targeting range
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, turretStats.targetingRange);
    }

    private void OnMouseDown()
    {
        currentTurretOnPointer = gameObject;
        buildManager.selectedTurret = currentTurretOnPointer;
        buildManager.selectBuiltTurret();
        tileWhenTowerWasClicked = tile.currentTile;
    }

    private void OnMouseUp()
    {
        if (buildManager.tileWithTurret.GetTurret() != null)
        {
            //här kan merge koden vara sen
            buildManager.deselectBuiltTurret();
            Debug.Log("deselect, Men kan köra merge också sen");
        }
        if (buildManager.tileWithTurret.GetTurret() == null)
        {
            if (buildManager.checkIfMouseIsOverATile())
            {
                Debug.Log("flytta turret");
                if (buildManager.tileWithTurret != null)
                {
                    buildManager.selectedTurret.transform.position = buildManager.tileWithTurret.transform.position;
                }
                else
                {
                    Debug.LogError("tileWithTurret is null!");
                }
                buildManager.tileWithTurret.SetTurretToNull();
                buildManager.deselectBuiltTurret();
                tile.SetTurretToNull();
            }
            else
            {
                buildManager.deselectBuiltTurret();
                Debug.Log("deselect");
            }
        }
    }
    public GameObject GetTurret()
    {
        return currentTurretOnPointer;
    }
}
