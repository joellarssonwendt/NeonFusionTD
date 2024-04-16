using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] List<Tile> listOfAllTiles;
    [SerializeField] private GameObject tempNormalTurret, tempFireTurret, tempIceTurret, tempLightningTurret, tempSuperNormalTurret, tempSuperFireTurret;
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject fireTurretPrefab;
    public GameObject superStandardTurretPrefab;
    public GameObject superFireTurretPrefab;
    private GameObject turretToBuild;
    public Tile tileObject;
    public GameObject selectedTurret;

    MergeManager mergeManager;
    void Start()
    {
        mergeManager = MergeManager.instance;
    }

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
        if (tileObject != null)
        {
            if (tileObject.GetTurret() != null)
            {
                //Debug.Log("get Turret är inte null");
            }
            else
            {
                //Debug.Log("get Turret == null");
            }  
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
                tileObject = tile;
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
       tempSuperNormalTurret.SetActive(false);
       tempSuperFireTurret.SetActive(false);
    }

    public void selectBuiltTurret()
    {
        if (turretToBuild == null)
        {
            Debug.Log("turretSelcted");
           if(selectedTurret.GetComponent<NormalTurret>() != null)
           {
                Debug.Log("normalTurretOnTileSelected");
                mergeManager.heldTurret = "normal";
                tempNormalTurret.SetActive(true);
           }
           else if (selectedTurret.GetComponent<FireTurret>() != null)
           {
                Debug.Log("FireTurretOnTileSelected");
                mergeManager.heldTurret = "fire";
                tempFireTurret.SetActive(true);
           }
            else if (selectedTurret.GetComponent<SuperFireTurret>() != null)
            {
                Debug.Log("SuperFireTurretOnTileSelected");
                mergeManager.heldTurret = "superfire";
                tempSuperFireTurret.SetActive(true);
            }
            else if (selectedTurret.GetComponent<SuperNormalTurret>() != null)
            {
                Debug.Log("SuperNormalTurretOnTileSelected");
                mergeManager.heldTurret = "supernormal";
                tempSuperNormalTurret.SetActive(true);
            }
        }
    }
    public void deselectBuiltTurret()
    {
        selectedTurret = null;
        mergeManager.heldTurret = null;
        deactivateTempTurretSprites();
    }
}
