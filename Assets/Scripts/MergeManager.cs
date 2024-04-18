using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;

    // Variables
    private GameObject mergeResult = null;

    // Upgrade Prefabs
    [SerializeField] private GameObject superKinetic, superFire, kineticFire;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en MergeManager!");
            return;
        }
        instance = this;
    }

    public bool CanMerge(GameObject heldTurret, GameObject tileTurret)
    {
        Debug.Log("TryMerge() " + heldTurret.tag + " + " + tileTurret.tag);

        if (heldTurret.CompareTag("normal") && tileTurret.CompareTag("normal"))
        {
            mergeResult = superKinetic;

            Destroy(heldTurret);
            Destroy(tileTurret);

            Debug.Log("Merge Successful!");
            return true;
        }

        if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("fire"))
        {
            mergeResult = superFire;

            Destroy(heldTurret);
            Destroy(tileTurret);

            Debug.Log("Merge Successful!");
            return true;
        }

        if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("fire")))
        {
            mergeResult = kineticFire;

            Destroy(heldTurret);
            Destroy(tileTurret);

            Debug.Log("Merge Successful!");
            return true;
        }

        return false;
    }

    public GameObject GetMergeResult()
    {
        return mergeResult;
    }
}
