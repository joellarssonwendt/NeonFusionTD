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
        //Debug.Log("Normal turret vald");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);

    }
    public void SelectIceTurret()
    {
        //Debug.Log("Ice turret vald");
        buildManager.SetTurretToBuild(buildManager.iceTurretPrefab);
    }
    public void SelectLightningTurret()
    {
        //Debug.Log("Lightning turret vald");
        buildManager.SetTurretToBuild(buildManager.lightningTurretPrefab);
    }
    public void SelectFireTurret()
    {
        //Debug.Log("fire turret vald");
        buildManager.SetTurretToBuild(buildManager.fireTurretPrefab);
    }
}
