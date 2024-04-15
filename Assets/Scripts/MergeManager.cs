using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;
    BuildManager buildManager;

    [Header("Variables")]
    public GameObject mergeResult;
    public string heldTurret;

    [Header("Prefabs")]
    public GameObject normalTurret, fireTurret, superNormalTurret, superFireTurret, superNormalFireTurret;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en MergeManager!");
            return;
        }
        instance = this;
        mergeResult = null;
        heldTurret = "";
    }

    public bool Merge(string tileTurretTag)
    {
        Debug.Log("Turret: " + heldTurret + " tileTurret: " + tileTurretTag);

        if (heldTurret == "normal" && tileTurretTag == "normal")
        {
            mergeResult = superNormalTurret;
            buildManager.SetTurretToBuildIsNull();
            Debug.Log("Merge Successful!");
            return true;
        }

        return false;
    }
}
