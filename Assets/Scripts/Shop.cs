using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    MergeManager mergeManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        mergeManager = MergeManager.instance;
    }
    public void SelectStandardTurret()
    {
        //Debug.Log("basic turret vald");
        mergeManager.heldTurret = "normal";
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);

    }
    public void SelectFireTurret()
    {
        //Debug.Log("fire turret vald");
        mergeManager.heldTurret = "fire";
        buildManager.SetTurretToBuild(buildManager.fireTurretPrefab);
    }
    public void SelectSuperStandardTurret()
    {
        //Debug.Log("superNormal turret vald");
        mergeManager.heldTurret = "supernormal";
        buildManager.SetTurretToBuild(buildManager.superStandardTurretPrefab);
    }
    public void SelectSuperFireTurret()
    {
        //Debug.Log("superfire turret vald");
        mergeManager.heldTurret = "superfire";
        buildManager.SetTurretToBuild(buildManager.superFireTurretPrefab);
    }
}
