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

    BuildManager buildManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildManager = BuildManager.instance;
    }

    public void OnMouseUp()
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
        turret = (GameObject)Instantiate(turretToBuild, transform.position, Quaternion.identity);
    }
    
    private void OnMouseEnter()
    {
        if(buildManager.GetTurretToBuild() != null)
        {
            spriteRenderer.color = grayColor;
        }
    }
    private void OnMouseExit()
    {
        spriteRenderer.color = whiteColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
