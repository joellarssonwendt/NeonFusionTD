using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;
    private BuildManager buildManager;

    // Variables
    public GameObject mergeResult;

    [SerializeField] private GameObject superNormalTurret, superFireTurret, superNormalFireTurret;

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
    }

    public bool Merge(GameObject tileTurret, GameObject heldTurret)
    {
        if (heldTurret.GetType() == typeof(NormalTurret) && tileTurret.GetType() == typeof(NormalTurret))
        {
            mergeResult = superNormalTurret;
            ClearTurrets(tileTurret, heldTurret);
            buildManager.SetTurretToBuildIsNull();
            return true;
        }

        if (heldTurret.GetType() == typeof(FireTurret) && tileTurret.GetType() == typeof(FireTurret))
        {
            mergeResult = superFireTurret;
            ClearTurrets(tileTurret, heldTurret);
            buildManager.SetTurretToBuildIsNull();
            return true;
        }

        if (heldTurret.GetType() == typeof(FireTurret) && tileTurret.GetType() == typeof(NormalTurret) || heldTurret.GetType() == typeof(NormalTurret) && tileTurret.GetType() == typeof(FireTurret))
        {
            mergeResult = superNormalFireTurret;
            ClearTurrets(tileTurret, heldTurret);
            buildManager.SetTurretToBuildIsNull();
            return true;
        }

        return false;
    }

    private void ClearTurrets(GameObject tileTurret, GameObject heldTurret)
    {
        Destroy(tileTurret);
        Destroy(heldTurret);
    }
}
