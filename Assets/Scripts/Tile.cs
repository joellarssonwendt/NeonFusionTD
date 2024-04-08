using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool hasTurretOnTile = false;
    private Color grayColor = Color.gray;
    private Color whiteColor = Color.white;
    private GameObject turret;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(turret != null)
        {
            Debug.Log("Can't place turret here.");
            return;
        }
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, Quaternion.identity);
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = grayColor;
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
