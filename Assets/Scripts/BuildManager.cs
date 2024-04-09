using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
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

    public void SetTurretToBUild (GameObject turret)
    {
        turretToBuild = turret;
    }
}
