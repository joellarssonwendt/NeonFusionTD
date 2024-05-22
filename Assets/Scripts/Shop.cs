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
        buildManager.SetTurretToBuild(buildManager.kineticTowerPrefab);

    }
    public void SelectIceTurret()
    {
        //Debug.Log("Ice turret vald");
        buildManager.SetTurretToBuild(buildManager.frostTowerPrefab);
    }
    public void SelectLightningTurret()
    {
        //Debug.Log("Lightning turret vald");
        buildManager.SetTurretToBuild(buildManager.shockTowerPrefab);
    }
    public void SelectFireTurret()
    {
        //Debug.Log("fire turret vald");
        buildManager.SetTurretToBuild(buildManager.fireTowerPrefab);
    }
}
