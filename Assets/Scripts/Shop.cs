using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        //Debug.Log("basic turret vald");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);

    }
    public void SelectFireTurret()
    {
        //Debug.Log("fire turret vald");
        buildManager.SetTurretToBuild(buildManager.fireTurretPrefab);
    }
}
