using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    // Cache
    public static MergeManager instance;
    BuildManager buildManager;

    // Variables
    private GameObject mergeResult = null;
    private bool normalNormalUnlocked, fireFireUnlocked, normalFireUnlocked, normalIceUnlocked, normalLightningUnlocked, iceIceUnlocked, iceLightningUnlocked, iceFireUnlocked, lightningFireUnlocked, lightningLightningUnlocked;

    // Upgrade Prefabs
    [SerializeField] private GameObject normalNormal, fireFire, normalFire, normalIce, normalLightning, iceIce, iceLightning, iceFire, lightningFire, lightningLightning;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en MergeManager!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public bool CanMerge(GameObject heldTurret, GameObject tileTurret)
    {
        if (PlayerStats.Crystals >= 10)
        {
            //combo 1
            if (heldTurret.CompareTag("normal") && tileTurret.CompareTag("normal"))
            {
                mergeResult = normalNormal;
                normalNormalUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 2
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("fire"))
            {
                mergeResult = fireFire;
                fireFireUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 3
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("fire")))
            {
                mergeResult = normalFire;
                normalFireUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 4
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("ice")))
            {
                mergeResult = normalIce;
                normalIceUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 5
            if (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("normal") || (heldTurret.CompareTag("normal") && tileTurret.CompareTag("lightning")))
            {
                mergeResult = normalLightning;
                normalLightningUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 6
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("ice"))
            {
                mergeResult = iceIce;
                iceIceUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 7
            if (heldTurret.CompareTag("ice") && tileTurret.CompareTag("lightning") || (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("ice")))
            {
                mergeResult = iceLightning;
                iceLightningUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 8
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("ice") || (heldTurret.CompareTag("ice") && tileTurret.CompareTag("fire")))
            {
                mergeResult = iceFire;
                iceFireUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 9
            if (heldTurret.CompareTag("fire") && tileTurret.CompareTag("lightning") || (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("fire")))
            {
                mergeResult = lightningFire;
                lightningFireUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            //combo 10
            if (heldTurret.CompareTag("lightning") && tileTurret.CompareTag("lightning"))
            {
                mergeResult = lightningLightning;
                lightningLightningUnlocked = true;
                PlayerStats.AddCrystals(-PlayerStats.mergeCost);
                Debug.Log("Combination found!");
                return true;
            }
            return false;
        }
        Debug.Log("Not enough bits to merge");
        return false;
    }

    public GameObject GetMergeResult()
    {
        return mergeResult;
    }

    public ReadOnlyCollection<bool> GetUnlockedList()
    {
        var bools = new List<bool>();
        bools.Add(normalNormalUnlocked);
        bools.Add(fireFireUnlocked);
        bools.Add(normalFireUnlocked);
        bools.Add(normalIceUnlocked);

        bools.Add(normalLightningUnlocked);
        bools.Add(iceIceUnlocked);
        bools.Add(iceLightningUnlocked);

        bools.Add(iceFireUnlocked);
        bools.Add(lightningFireUnlocked);
        bools.Add(lightningLightningUnlocked);

        var readOnlyBools = new ReadOnlyCollection<bool>(bools);
        return readOnlyBools;
    }

    public void UpdateUnlockedList()
    {
        GetUnlockedList();
    }
}
