using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color grayColor = Color.gray;
    private Color whiteColor = Color.white;
    public bool isOverATile = false;
    private GameObject turret;
    public GameObject currentTile;

    BuildManager buildManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
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

        if (buildManager.checkIfMouseIsOverATile())
        {
            Debug.Log("checkIfMouseIsOverATile");
        }
    }
    public void PlaceTurret()
    {
        if(turret != null)
        {
            Debug.Log("Already a turret here");
            buildManager.SetTurretToBuildIsNull();
            return;
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, currentTile.transform.position, Quaternion.identity);
        buildManager.SetTurretToBuildIsNull();
    }
    public void RemoveMisplacedTurret()
    {
            Debug.Log("Can't place turret here.");
            buildManager.SetTurretToBuildIsNull();
            return;
    }
    private void OnMouseEnter()
    {
        isOverATile = true;
        currentTile = gameObject;
        Debug.Log("currentTile is this Tile");
        if (buildManager.GetTurretToBuild() != null)
        {
            spriteRenderer.color = grayColor;
        }
    }
    private void OnMouseExit()
    {
        Debug.Log("currentTile is NULL");
        currentTile = null;
        spriteRenderer.color = whiteColor;
        isOverATile = false;
    }
}
