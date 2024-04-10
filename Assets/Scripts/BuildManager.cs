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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en BuildManager");
            return;
        }
        instance = this;
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
}
