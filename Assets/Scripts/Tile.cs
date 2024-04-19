using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color newGray = new Color(20, 20, 20, 1);
    private Color newLightGray = new Color(54, 54, 54, 1);
    public bool isOverATile = false;
    [SerializeField] private GameObject turret;
    public GameObject currentTile;

    BuildManager buildManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && buildManager.isRaycastHittingTile() && currentTile != null && buildManager.GetTurretToBuild() != null)
        {
            PlaceTurret();
        }
        else if (Input.GetMouseButtonUp(0) && !buildManager.isRaycastHittingTile() && buildManager.GetTurretToBuild() != null)
        {
            RemoveMisplacedTurret();
        }
    }
    public void PlaceTurret()
    {
        if (turret != null || PlayerStats.Chrystals < PlayerStats.towerCost)
        {
            buildManager.SetTurretToBuildIsNull();
            return;
        }

        PlayerStats.Chrystals -= PlayerStats.towerCost;
        Debug.Log("Turret Built! Chrystals left: " + PlayerStats.Chrystals);
        Vector3 newCalculatedTowerPosition = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, newCalculatedTowerPosition, Quaternion.identity);
        buildManager.SetTurretToBuildIsNull();
    }
    public void RemoveMisplacedTurret()
    {
        buildManager.SetTurretToBuildIsNull();
        return;
    }
    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() != null)
        {
            // spriteRenderer.color = newLightGray;
        }
        isOverATile = true;
        currentTile = gameObject;
    }
    private void OnMouseExit()
    {
        // spriteRenderer.color = newGray;
        currentTile = null;
        isOverATile = false;
    }
    public GameObject GetTurret()
    {
        return turret;
    }
    public void SetTurret(GameObject turret)
    {
        this.turret = turret;
    }

    public string GetTurretTag()
    {
        return turret.tag;
    }
    public void SetTurretToNull()
    {
        turret = null;
    }
}
