using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color newGray = new Color(20, 20, 20, 1);
    private Color newLightGray = new Color(54, 54, 54, 1);
    public bool isOverATile = false;
    private GameObject turret;
    public GameObject currentTile;

    BuildManager buildManager;
    MergeManager mergeManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
        mergeManager = MergeManager.instance;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && buildManager.checkIfMouseIsOverATile() && currentTile != null && buildManager.GetTurretToBuild() != null) 
        {
            PlaceTurret();
        }
        else if (Input.GetMouseButtonUp(0) && !buildManager.checkIfMouseIsOverATile() && buildManager.GetTurretToBuild() != null)
        {
            RemoveMisplacedTurret();
        }
    }
    public void PlaceTurret()
    {
        if(turret != null)
        {
            buildManager.SetTurretToBuildIsNull();
            return;
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, currentTile.transform.position, Quaternion.identity);
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
