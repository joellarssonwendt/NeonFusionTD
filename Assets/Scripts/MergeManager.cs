using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;

    // Variables
    private GameObject mergeResult = null;
    private bool superKineticUnlocked, superFireUnlocked, kineticFireUnlocked;

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
            superKineticUnlocked = true;
            Debug.Log("Merge Successful!");
            return true;
        }

        if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("fire"))
        {
            mergeResult = superFire;
            superFireUnlocked = true;
            Debug.Log("Merge Successful!");
            return true;
        }

        if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("fire")))
        {
            mergeResult = kineticFire;
            kineticFireUnlocked = true;
            Debug.Log("Merge Successful!");
            return true;
        }

        return false;
    }

    public GameObject GetMergeResult()
    {
        return mergeResult;
    }

    public ReadOnlyCollection<bool> GetUnlockedList()
    {
        var bools = new List<bool>();
        bools.Add(superKineticUnlocked);
        bools.Add(superFireUnlocked);
        bools.Add(kineticFireUnlocked);
        var readOnlyBools = new ReadOnlyCollection<bool>(bools);
        return readOnlyBools;
    }
}
