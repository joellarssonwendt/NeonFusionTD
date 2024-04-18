using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;

    [Header("Variables")]
    public GameObject mergeResult;
    public string heldTurret;

    [Header("Prefabs")]
    public GameObject normalTurret, fireTurret, superNormalTurret, superFireTurret, superNormalFireTurret;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en MergeManager!");
            return;
        }
        instance = this;
        mergeResult = null;
        heldTurret = null;
    }

    public bool TryMerge(GameObject tileTurret)
    {
        Debug.Log("Turret: " + heldTurret + " tileTurret: " + tileTurret.tag);

        if (heldTurret == "normal" && tileTurret.CompareTag("normal"))
        {
            mergeResult = superNormalTurret;
            Debug.Log("Merge Successful!");
            Destroy(tileTurret);
            return true;
        }

        if (heldTurret == "fire" && tileTurret.CompareTag("fire"))
        {
            mergeResult = superFireTurret;
            Debug.Log("Merge Successful!");
            Destroy(tileTurret);
            return true;
        }

        if (heldTurret == "fire" && tileTurret.CompareTag("normal") || (heldTurret == "normal" && tileTurret.CompareTag("fire")))
        {
            mergeResult = superNormalFireTurret;
            Debug.Log("Merge Successful!");
            Destroy(tileTurret);
            return true;
        }

        return false;
    }
}
