using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] List<Tile> listOfAllTiles;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret;
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject fireTurretPrefab;
    private GameObject turretToBuild;
    public Tile tileWithTurret;
    public GameObject selectedTurret;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en BuildManager");
            return;
        }
        instance = this;
    }
    private void Update()
    {
        if(turretToBuild != null)
        {
            Debug.Log("Turret to build är inte null");
        }
        else
        {
            Debug.Log("TurretToBuild == null");
        }
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }
    public void SetTurretToBuildIsNull()
    {
        deactivateTempTurretSprites();
        turretToBuild = null;
    }
    public bool checkIfMouseIsOverATile()
    {
        foreach (Tile tile in listOfAllTiles)
        {
            if(tile.isOverATile == true)
            {
                tileWithTurret = tile;
                return true;
            }
        }
        return false;
    }

    public void deactivateTempTurretSprites()
    {
        tempNormalTurret.SetActive(false);
        tempFireTurret.SetActive(false);
       // tempIceTurret.SetActive(false);
       // tempLightningTurret.SetActive(false);
    }

    public void selectBuiltTurret()
    {
        if (turretToBuild == null)
        {
            Debug.Log("turretSelcted");
           if(selectedTurret.GetComponent<NormalTurret>() != null)
           {
                Debug.Log("normalTurretOnTileSelected");
                tempNormalTurret.SetActive(true);
           }
           else if (selectedTurret.GetComponent<FireTurret>() != null)
           {
                Debug.Log("FireTurretOnTileSelected");
                tempFireTurret.SetActive(true);
            }
        }
    }
    public void deselectBuiltTurret()
    {
        selectedTurret = null;
        deactivateTempTurretSprites();
    }
}
