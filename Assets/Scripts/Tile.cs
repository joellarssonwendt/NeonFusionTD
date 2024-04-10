using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool hasTurretOnTile = false;
    private Color grayColor = Color.gray;
    private Color whiteColor = Color.white;
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
        if(Input.GetMouseButtonUp(0) && currentTile != null) 
        {
            PlaceTurret();
        }
    }
    public void PlaceTurret()
    {
        if(buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        if(turret != null)
        {
            Debug.Log("Can't place turret here.");
            return;
        }
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, currentTile.transform.position, Quaternion.identity);
    }
    
    private void OnMouseEnter()
    {
        currentTile = gameObject;
        if(buildManager.GetTurretToBuild() != null)
        {
            spriteRenderer.color = grayColor;
        }
    }
    private void OnMouseExit()
    {
        currentTile = null;
        spriteRenderer.color = whiteColor;
    }
}
